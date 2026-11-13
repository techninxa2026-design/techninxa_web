#!/usr/bin/env python3
"""Static regression checks for IT & IoT project visuals and team avatars."""
from pathlib import Path
import re
import sys
import xml.etree.ElementTree as ET

root = Path(__file__).resolve().parents[1]
media_file = root / "techninxa/Services/ItIotMedia.cs"
source = media_file.read_text(encoding="utf-8")
assets = root / "techninxa/wwwroot"
problems = []

previews = [
    "software", "commerce", "health", "education", "jobs", "ai",
    "iot", "security", "inventory", "finance", "automotive", "design", "mobile",
]
for kind in previews:
    path = assets / "assets/it-iot/previews" / (kind + ".svg")
    if not path.is_file():
        problems.append(f"Missing {kind} project preview: {path}")

# SVG parsing catches broken tag trees and invalid XML attributes, which
# the .NET compiler alone cannot detect.
for svg in sorted((assets / "assets/it-iot").rglob("*.svg")):
    try:
        ET.parse(svg)
    except ET.ParseError as exc:
        problems.append(f"Malformed SVG {svg.relative_to(root)}: {exc}")

for index in range(1, 13):
    path = assets / "assets/it-iot/avatars" / f"avatar-{index:02d}.svg"
    if not path.is_file():
        problems.append(f"Missing team avatar {index:02d}")

# All explicit case-study asset paths should exist in source control.
case_images = set(re.findall(r'Real\("(/(?:assets|images)/[^"]+)"', source))
for url in sorted(case_images):
    if not (assets / url.lstrip("/")).is_file():
        problems.append(f"Named case-study cover not present: {url}")

home = (root / "techninxa/Views/Home/Index.cshtml").read_text(encoding="utf-8")
library = (root / "techninxa/Views/Home/Projects.cshtml").read_text(encoding="utf-8")
layout = (root / "techninxa/Views/Shared/_Layout.cshtml").read_text(encoding="utf-8")
css = root / "techninxa/wwwroot/css/it-iot-media.css"
js = root / "techninxa/wwwroot/js/it-iot-media.js"

checks = [
    ("project covers in IT homepage", "ItIotMedia.ResolveProject" in home),
    ("avatars in IT homepage", "ItIotMedia.AvatarFor" in home),
    ("project covers in project library", "ItIotMedia.ResolveProject" in library),
    ("responsive styles attached", "it-iot-media.css" in layout and css.is_file()),
    ("fallback script attached", "it-iot-media.js" in layout and js.is_file()),
]
problems.extend(f"Missing {description}" for description, ok in checks if not ok)

if problems:
    for problem in problems:
        print("FAIL:", problem, file=sys.stderr)
    raise SystemExit(1)

print(f"OK: {len(previews)} local subject-specific interface previews, "
      f"12 local avatars and {len(case_images)} known case-study assets.")
