#!/usr/bin/env python3
"""Validate 1:1 coverage between Cytech's numbered 2026 catalogue and image mapping."""
from pathlib import Path
import re
import sys

root = Path(__file__).resolve().parents[1]
catalogue = (root / "techninxa/Services/CytechCatalogue.cs").read_text(encoding="utf-8")
media = (root / "techninxa/Services/CytechProductMedia.cs").read_text(encoding="utf-8")

sections = [int(n) for n in re.findall(r'new CatalogueCategory\(\s*(\d+),', catalogue)]
photos = re.findall(r'\[(\d+)\]\s*=\s*new CytechProductPhoto\("([^"]+)"', media)
ids = [int(n) for n, _ in photos]

problems = []
if len(sections) != 38 or sorted(sections) != list(range(1, 39)):
    problems.append("Catalogue must contain unique numbered sections 1 through 38")
if len(ids) != 38 or sorted(ids) != list(range(1, 39)):
    problems.append("Photo mappings must contain unique numbered sections 1 through 38")
if sorted(sections) != sorted(ids):
    problems.append("Missing/extraneous product photo mappings")
if any(not url.startswith("https://") for _, url in photos):
    problems.append("Product images must use HTTPS")
for rel in [
    "techninxa/Views/Shared/_CatalogueProductCard.cshtml",
    "techninxa/wwwroot/css/cytech-product-media.css",
    "techninxa/wwwroot/js/cytech-product-media.js",
]:
    if not (root / rel).exists():
        problems.append(f"Missing presentation asset: {rel}")

if problems:
    for problem in problems:
        print("FAIL:", problem, file=sys.stderr)
    raise SystemExit(1)
print(f"OK: {len(ids)} of {len(sections)} catalogue sections have mapped product photos.")
