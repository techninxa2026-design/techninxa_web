# Cytech Engineering: corporate routes and product catalogue

## Source of truth

The uploaded **Cytech Engineering Industrial Product Catalogue 2026** provides:

- 38 numbered industrial product sections, printed 01-38 (PDF pages 2-39)
- Five company services and seven industries (PDF page 40)
- Public phone, email and physical address (PDF page 40)

The catalogue does **not** supply prices, live stock, verified customer projects, authorized-distributor contracts, certificates, employee counts or service SLAs. Do not invent those in public copy.

## Main navigation

- `/` — Catalogue-backed industrial landing page
- `/about` — Verified industrial company information and service coverage
- `/products` — All 38 product categories; text search and family filtering
- `/products/{slug}` — Numbered catalogue category with product-type list and brand references
- `/services` — The five verified industrial engineering services
- `/it-iot-services` — Existing IT/IoT portfolio preserved from earlier website
- `/contact` — Requirements and quotation requests persisted to `ContactMessages`

The earlier division routes remain for compatibility:

- `/electrical-supply-installation` — Electrical and power categories mapped to the catalogue
- `/networking-services` — Network and structured cabling categories mapped to the catalogue
- `/manpower-services` — Existing placeholder, **not** described by the supplied industrial catalogue
- `/blog` — Existing placeholder
- `/it-iot-services` — Original portfolio with dynamic EF-backed project and team content

## Verified services

1. Supply of Industrial Products
2. System Integration
3. Installation & Commissioning
4. Maintenance & Support
5. Technical Consultation

## Verified industries

Manufacturing; Food & Beverage; Pharmaceuticals; Oil & Gas; Power & Energy; Water & Wastewater; Building & Infrastructure.

## Verified contact information

**Email:** info@cytechbd.com

**Phones:** +880 1711 015660 and +880 1790 379729

**Address:** BSCIC Electronic Complex, Level 6, Avenue 4, Road 3, Section 7, Mirpur, Dhaka, Bangladesh

**Website printed in PDF:** www.cytechbd.com

## Content ownership and implementation

- Catalogue data and source-page mapping: `techninxa/Services/CytechCatalogue.cs`
- Corporate content/routes and inquiry persistence: `techninxa/Controllers/SitePagesController.cs`
- Industrial division landing pages: `techninxa/Controllers/DivisionsController.cs`
- Corporate views: `techninxa/Views/SitePages/*.cshtml`
- Industrial division views: `techninxa/Views/Divisions/*.cshtml`
- New UI stylesheet: `techninxa/wwwroot/css/cytech-catalogue.css`
- Live filters: `techninxa/wwwroot/js/cytech-catalogue.js`
- Abstract vector illustration: `techninxa/wwwroot/assets/cytech-industrial-scene.svg`

The product card categories group the catalogue for navigation. A family name is **not** a distinct catalogue section. Specific models, ratings and part numbers require direct sales confirmation.

Contact requests use the existing EF Core `ContactMessages` schema. No database migration is necessary for these pages. Preserve controller-side antiforgery validation.

## QA

The GitHub Actions workflow `.github/workflows/aspnet-core-ci.yml` builds the .NET 10 solution. Visual and end-to-end checks in the production environment are still needed, especially the live contact submission. Legacy administrative security and missing-view issues are separate pre-existing tasks and are not claimed to be fixed by the corporate catalogue redesign.
