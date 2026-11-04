# Cytech Engineering — website design system

The website uses the supplied Cytech orange-and-white mark (already available at `techninxa/wwwroot/assets/cytechlogo.jpg`).

## Core tokens

| Token | Value | Purpose |
| --- | --- | --- |
| `--cy-orange` | `#FF4B0B` | Primary actions, active navigation, emphasis |
| `--cy-orange-dark` | `#D93B06` | Hover / pressed states |
| `--cy-coral` | `#FF8652` | Secondary highlights and glows |
| `--cy-ink` | `#1A1B20` | Primary text |
| `--cy-deep` | `#15171C` | Dark hero cards and footer |
| `--cy-paper` | `#FFFAF6` | Warm page background |
| `--cy-cream` | `#FFF2E9` | Secondary surfaces |
| `--cy-line` | `#EFDFD4` | Borders |

The legacy `--blue` / `--sky` variable names remain as aliases to the Cytech colors, so existing component CSS still resolves correctly.

## Style loading order

1. `wwwroot/css/style.css`: original site components, animations, and responsive layout.
2. Per-view `@section Styles`: Razor-specific legacy CSS.
3. `wwwroot/css/cytech-theme.css`: brand-wide tokens and shared component overrides.

The shared layout is selected in `Views/_ViewStart.cshtml`, except for standalone case studies and the scheduling page, which explicitly set `Layout = null`. Do not nest full HTML documents in `_Layout.cshtml`.

`wwwroot/css/person-portfolio.css` retains personal portfolio layouts but uses the Cytech accent. Standalone case studies intentionally preserve the showcased products' own color identities.

## Changing the look later

Update the Cytech tokens in `cytech-theme.css` first, then component-specific rules. Avoid appending more duplicate rules to `style.css` or changing C# controllers to make visual adjustments.

## Manual verification

- Homepage at desktop/mobile widths: header, orange hero, actual Cytech logo, services, projects, contact form, footer
- `/Projects`: tabs, cards, filters, active states
- Login, dashboard, project add/edit/list, teams and schedule: form focus, navigation, tables, modal panels
- Standalone project detail pages and company chat login: no nested layout or missing styles
- Keyboard navigation and reduced-motion settings

## Deployment notes

This branch is a visual redesign. No application models, EF migrations, or SQL Server settings were changed. Build and browser smoke tests should be run in the target .NET 10 environment before IIS deployment.

Legacy canonical links, social profiles, and contact email addresses are intentionally left unchanged until the replacement Cytech-owned destinations are confirmed. These are branding/migration tasks separate from the visual theme.
