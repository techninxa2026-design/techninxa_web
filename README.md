# Cytech Engineering website

ASP.NET Core MVC (.NET 10), Razor, EF Core SQL Server, and SignalR.

## Public website

- `/`: industrial engineering corporate homepage
- `/about`: catalogue-verified company profile and industries
- `/services`: product supply, integration, installation/commissioning, maintenance/support, technical consultation
- `/products`: searchable/filterable industrial product catalogue (38 source sections)
- `/products/{slug}`: product-section details, listed component types, brand references and quotation links
- `/contact`: corporate inquiry form (saves into existing `ContactMessages` table)
- `/networking-services`, `/electrical-supply-installation`: focused product collections
- `/it-iot-services`: existing IT/IoT portfolio and original administrative backend (preserved)

## Source of corporate information

The supplied **Cytech Engineering Industrial Product Catalogue 2026** is the authority for the public industrial product range. Its numbered sections 01-38 occupy PDF pages 2-39. Page 40 provides the company description, five services, seven industry areas and contact information. `techninxa/Services/CytechCatalogue.cs` keeps that source-aligned data in one place.

Product families are navigation groupings created for this website. They are **not** additional claims in the PDF. Brand names are catalogue references, not evidence of authorized dealership status. No prices, SKU stock levels or certifications have been invented.

## Local build

```sh
dotnet restore techninxa.sln
dotnet build techninxa.sln --configuration Release
dotnet run --project techninxa/techninxa.csproj
```

The application needs SQL Server connection settings from local/environment configuration. **Do not commit credentials.** The corporate home, About, Services and Products pages do not query SQL Server; contact submission does.

## Styling

- `wwwroot/css/cytech-catalogue.css`: new, responsive corporate and catalogue UI
- `Services/CytechProductMedia.cs`: 38 matching product-photo references and original source links, aligned with catalogue section numbers
- `Views/Shared/_CatalogueProductCard.cshtml`: shared product image/card UI across the catalogue, home, and division routes
- `wwwroot/css/cytech-product-media.css`: responsive product photography layout
- `wwwroot/js/cytech-product-media.js`: fallback image loading when a third-party host fails
- `scripts/verify_catalogue_media.py`: validates complete image mapping for all 38 sections
- `wwwroot/js/cytech-catalogue.js`: searchable filters and deep-link family selection
- `wwwroot/css/cytech-theme.css`: existing corporate theme tokens
- Existing IT/IoT pages, their styles and admin workflows remain separate.

## Product-image rights and provenance

The photographs in `CytechProductMedia.cs` show representative equipment from external publishers. They are **not** Cytech's own photography and do not verify live stock, model availability or manufacturer authorization. The detail page credits each image publisher. Since third-party image-hosting and reuse permissions can change, obtain permission or replace these URLs with Cytech-owned/approved assets under `wwwroot` before production. If a URL fails to load, the frontend attempts a matching industrial-family fallback image.

## Deployment checklist

1. Configure the SQL Server connection string securely for the target deployment.
2. Check `/`, `/about`, `/services`, `/products`, a product detail URL and `/contact` at desktop and mobile widths.
3. Test contact validation, antiforgery verification and saving inquiries into the existing `ContactMessages` table.
4. Confirm email addresses, phone numbers, DNS/canonical URLs and actual product availability before going live.
5. Run GitHub Actions ASP.NET Core MVC CI for compile validation and full photo-mapping coverage.
6. Verify image load/attribution in a real browser and confirm the website has permission to reuse the images.

Known legacy limitations outside the catalogue redesign: older IT/IoT admin/portfolio routes have missing Razor views and should be audited separately. The CI build reports pre-existing compiler warnings rather than treating all warnings as errors.
