"""Run only against the isolated CI database, never against live customer data."""
import os, time, urllib.request, urllib.error, urllib.parse, http.cookiejar, re
from html import unescape
base = "http://127.0.0.1:" + os.environ.get("WEB_PORT", "8105")
class NoRedirect(urllib.request.HTTPRedirectHandler):
    def redirect_request(self, *args, **kwargs):
        return None
def client():
    return urllib.request.build_opener(NoRedirect(), urllib.request.HTTPCookieProcessor(http.cookiejar.CookieJar()))
def request(opener, path, expected, data=None, token=None):
    headers = {"X-Forwarded-Proto": "https"}
    if data is not None:
        data = dict(data)
        if token: data["__RequestVerificationToken"] = token
        data = urllib.parse.urlencode(data).encode()
    req = urllib.request.Request(base + path, data=data, headers=headers)
    try:
        response = opener.open(req, timeout=15)
    except urllib.error.HTTPError as err:
        response = err
    body = response.read().decode(errors="replace")
    assert response.code in expected, (path, response.code, expected, body[:250])
    print(path, response.code)
    return response, body
def token_from(body):
    match = re.search(r'name="__RequestVerificationToken"[^>]*value="([^"]+)"', body)
    assert match, "Missing anti-forgery token"
    return unescape(match.group(1))
# urllib correctly refuses Secure cookies over internal HTTP; use an HTTPS-only
# cookie policy override for this private CI loopback connection.
class LoopbackPolicy(http.cookiejar.DefaultCookiePolicy):
    def __init__(self):
        super().__init__(secure_protocols=("https","wss","http"))
def client():
    return urllib.request.build_opener(NoRedirect(), urllib.request.HTTPCookieProcessor(http.cookiejar.CookieJar(policy=LoopbackPolicy())))
anon = client()
for attempt in range(45):
    try:
        request(anon, "/health/ready", (200,))
        break
    except (OSError, AssertionError):
        time.sleep(2)
else:
    raise RuntimeError("Application not ready")
for path in ["/","/about","/services","/products","/contact","/networking-services","/electrical-supply-installation","/it-iot-services","/Projects","/makeschedule","/AdminAccess","/CompanyChat","/health"]:
    request(anon,path,(200,))
request(anon,"/products/not-a-real-product",(404,))
request(anon,"/Dashboard",(302,))
request(anon,"/timeslots",(302,))
request(anon,"/Team",(302,))
request(anon,"/uploads/chat/nonexistent.pdf",(401,))
request(anon,"/companyChatHub/negotiate?negotiateVersion=1",(401,),{})
request(anon,"/contact",(400,),{})
_, contact = request(anon,"/contact",(200,))
token = token_from(contact)
request(anon,"/Home/Add",(401,),{"startTime":"09:00","endTime":"10:00"},token)
request(anon,"/Home/Delete",(401,),{"id":"1"},token)
request(anon,"/Home/Toggle",(401,),{"id":"1"},token)
admin = client()
_, login = request(admin,"/AdminAccess",(200,))
request(admin,"/Home/Login",(302,),{"Username":"qa@example.test","Password":os.environ["SMOKE_ADMIN_PASSWORD"]},token_from(login))
for path in ["/Dashboard","/meetingstojoin","/clientsmesseges","/timeslots","/setholydays","/Team","/ProjectList","/InsertProject","/Team/Create"]:
    request(admin,path,(200,))
print("Release smoke checks passed.")
