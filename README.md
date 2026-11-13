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
- `wwwroot/assets/cytech-industrial-scene.svg`: original technical illustration (not a product photograph)
- `wwwroot/js/cytech-catalogue.js`: searchable filters and deep-link family selection
- `wwwroot/css/cytech-theme.css`: existing corporate theme tokens
- Existing IT/IoT pages, their styles and admin workflows remain separate.

## Deployment checklist

1. Configure the SQL Server connection string securely for the target deployment.
2. Check `/`, `/about`, `/services`, `/products`, a product detail URL and `/contact` at desktop and mobile widths.
3. Test contact validation, antiforgery verification and saving inquiries into the existing `ContactMessages` table.
4. Confirm email addresses, phone numbers, DNS/canonical URLs and actual product availability before going live.
5. Run GitHub Actions ASP.NET Core MVC CI for compile validation.

Known legacy limitations outside the catalogue redesign: older IT/IoT admin/portfolio routes have missing Razor views and should be audited separately. The CI build reports pre-existing compiler warnings rather than treating all warnings as errors.
