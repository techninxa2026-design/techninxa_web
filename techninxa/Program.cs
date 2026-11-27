using System.Net;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Techninxa;
using Techninxa.Hubs;
using Techninxa.Services;

var migrateOnly = args.Contains("--migrate");
var builder = WebApplication.CreateBuilder(args.Where(x => x != "--migrate").ToArray());
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connection))
    throw new InvalidOperationException("Set ConnectionStrings__DefaultConnection.");
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddDistributedMemoryCache();
var edge = builder.Configuration.GetValue<bool>("ReverseProxy:EdgeTerminatesHttps");
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Techninxa.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = edge ? CookieSecurePolicy.Always : CookieSecurePolicy.SameAsRequest;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = edge ? CookieSecurePolicy.Always : CookieSecurePolicy.SameAsRequest;
});
var protection = builder.Services.AddDataProtection().SetApplicationName("techninxa-web");
var keys = builder.Configuration["DataProtection:KeyDirectory"];
if (!string.IsNullOrWhiteSpace(keys))
{
    Directory.CreateDirectory(keys);
    protection.PersistKeysToFileSystem(new DirectoryInfo(keys));
}
if (edge)
{
    var proxy = builder.Configuration["ReverseProxy:KnownProxy"];
    if (!IPAddress.TryParse(proxy, out var address))
        throw new InvalidOperationException("Set ReverseProxy__KnownProxy to the trusted Docker gateway.");
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor;
        options.KnownProxies.Add(address);
        options.ForwardLimit = 1;
    });
}
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    foreach (var (name, limit) in new[] { ("login", 10), ("public-write", 20) })
        options.AddPolicy(name, context => RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = limit, Window = TimeSpan.FromMinutes(1), QueueLimit = 0
            }));
});
var app = builder.Build();
if (migrateOnly)
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();
    await AdminCredentials.BootstrapAsync(db, app.Configuration);
    return;
}
if (edge) app.UseForwardedHeaders();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
if (!edge) app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
    await next();
});
app.UseRouting();
app.UseSession();
// Chat attachments are not public website assets.
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/uploads/chat", StringComparison.OrdinalIgnoreCase) &&
        context.Session.GetString("CompanyChatLoggedIn") != "true")
    {
        context.Response.StatusCode = 401;
        return;
    }
    if (context.Request.Path.StartsWithSegments("/uploads/chat", StringComparison.OrdinalIgnoreCase))
        context.Response.Headers.CacheControl = "private, no-store";
    await next();
});
app.UseStaticFiles();
app.UseRateLimiter();
app.UseAuthorization();
app.Map("/error", () => Results.Problem("An unexpected error occurred.", statusCode: 500));
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapGet("/health/ready", async (ApplicationDbContext db, CancellationToken ct) =>
{
    try
    {
        if (await db.Database.CanConnectAsync(ct) && !(await db.Database.GetPendingMigrationsAsync(ct)).Any())
            return Results.Ok(new { status = "ready" });
    }
    catch (Exception) when (!ct.IsCancellationRequested) { }
    return Results.StatusCode(503);
});
app.MapStaticAssets();
app.MapHub<CompanyChatHub>("/companyChatHub");
app.MapControllerRoute("it-iot-services", "it-iot-services", new { controller = "Home", action = "Index" }).WithStaticAssets();
app.MapControllerRoute("default", "{controller=SitePages}/{action=Home}/{id?}").WithStaticAssets();
app.Run();
public partial class Program { }
