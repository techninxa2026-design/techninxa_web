# Cytech Engineering service-division navigation

## Corporate header

The shared `Views/Shared/_Layout.cshtml` exposes four divisions:

| Division | Route | Implementation |
| --- | --- | --- |
| Electrical Supply & Installation | `/electrical-supply-installation` | Blank placeholder |
| Manpower Services | `/manpower-services` | Blank placeholder |
| Networking Services | `/networking-services` | Blank placeholder |
| IT & IoT Services | `/it-iot-services` | Existing MVC Home/Index |

The three blank pages are handled by `Controllers/DivisionsController.cs` and the reusable `Views/Divisions/Placeholder.cshtml`. Only a section title and a short placeholder message are shown. No service descriptions or business claims have been invented.

`Program.cs` maps `/it-iot-services` to the existing `Home.Index` action. The corporate root `/` is handled by `SitePagesController.Home` and is intentionally a branded placeholder. The former `Home/Index` destination redirects to `/it-iot-services`. Existing controllers, project routes, booking APIs and database actions are unchanged.

## Floating IT & IoT navigation

On the IT landing page `/it-iot-services` and `/Projects` routes, the previous top menu items now appear as a vertical floating capsule on the left:

- Home: `/it-iot-services`
- Services: `/it-iot-services#services`
- Projects: `/Projects`
- About: `/it-iot-services#about`
- Contact: `/it-iot-services#contact`

On narrow/mobile viewports the capsule moves to the bottom as a horizontal dock so it does not cover the main content. The `cytech-navigation.js` script manages the corporate hamburger and active item in the IT rail; both use existing navigation destinations, without altering form logic.

## Source map

- `Views/Shared/_Layout.cshtml`: corporate header, conditional IT rail, footer division links
- `wwwroot/css/cytech-divisions.css`: desktop/mobile header, floating capsule and placeholders
- `wwwroot/js/cytech-navigation.js`: hamburger and rail-active behavior
- `Controllers/DivisionsController.cs`: placeholder routes
- `Views/Divisions/Placeholder.cshtml`: intentionally blank division content
- `Program.cs`: IT route alias

## Local checks

Run `dotnet restore` and `dotnet build` using the .NET 10 SDK. Check all four header destinations; test the hamburger at mobile widths; check the floating dock at desktop and mobile widths; confirm `/Projects` and `/it-iot-services#services` work. Confirm the three placeholders do not trigger the existing IT database queries.

Case-study pages with `Layout = null` retain their independent project design and are deliberately not given the floating navigation yet.


## Corporate top navigation (updated)

The header now reads **Home · About · Contact · Services ▾ · Blog · Products**.

- **Home** uses `/` (a standalone corporate placeholder; the previous software homepage is only at `/it-iot-services`).
- **About** uses `/about` and **Contact** uses `/contact`. Both are separate corporate placeholder pages.
- **Services** opens a responsive, accessible dropdown with the four division routes described above. Click/touch on mobile, hover/click on desktop, and keyboard activation are supported.
- **Blog** at `/blog` and **Products** at `/products` are minimal placeholders, implemented by `SitePagesController`.
- The IT & IoT floating rail remains available on the existing IT homepage and Projects routes, independent of the main corporate menu.

## Corporate/IT separation (latest)

The corporate pages `/`, `/about`, and `/contact` now contain approved content and design; `/blog` and `/products` remain standalone placeholders. Home and About do not query the database. Contact saves inquiries to the existing message table. The existing software, IoT, projects, people, technology, and contact sections remain intact at `/it-iot-services` and its original project URLs. The floating IT capsule is rendered only on `/it-iot-services` and `/Projects` routes. `/Home/Index` and `/Home` redirect to the dedicated IT route to prevent duplicate public entry points.

The corporate header and footer now link to `/about` and `/contact`, not to the IT website's anchor sections. The IT-specific floating capsule still links to `/it-iot-services#about` and `/it-iot-services#contact`.


## Approved corporate page rollout

The corporate Home, About and Contact concepts were approved and implemented from the original three-page prototype:

- `/` — the full corporate landing page, with four service cards, process steps, industries, project showcase placeholders, and quotations CTA
- `/about` — company story, four-division capability summary, operating approach, values, and trust/credentials sections
- `/contact` — responsive corporate requirement form with validation, CSRF protection, honeypot, per-division preselection and confirmation banner

Styles are scoped in `wwwroot/css/cytech-corporate.css`, which is loaded only for the three corporate pages. The existing IT pages under `/it-iot-services` keep their previous CSS, data-driven content and floating rail.

**Contact submission:** `SitePagesController.SubmitInquiry` saves validated inquiries to the existing `ContactMessages` table via `ApplicationDbContext`. The company, phone, service division and inquiry type are included in the message text. No schema changes or migrations are needed, and no SMTP/email notification is claimed.

Verified leadership identities, registration/credential documents, site images and corporate contact details are still required before those placeholders can be replaced with company-specific facts.

**Deployment checks:** In the target ASP.NET Core 10 environment, run `dotnet build`; visit `/`, `/about`, `/contact`, `/blog`, `/products`, and `/it-iot-services`; test desktop and mobile layouts and send a validation-tested contact inquiry to ensure it appears in the existing admin messages view. Keep your existing `appsettings.json` connection string local and do not commit credentials.

## Public contact details and Networking Services

Public company contact details supplied by Cytech:

- Phone: **01711015660** (`tel:+8801711015660`)
- Email: **cytech@gmail.com**
- City/location: **Dhaka, Bangladesh** (no street address supplied)

These now appear on About, Contact and the corporate footer. The IT/shared header's phone CTA also uses the updated number. The standalone `/networking-services` route now renders a dedicated overview of common business networking services, with no invented certifications, customers or performance claims. Its quote links preselect Networking Services on the corporate contact form.
