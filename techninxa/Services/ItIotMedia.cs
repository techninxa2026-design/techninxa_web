using System;
using Techninxa.Models;

namespace Techninxa.Services;

/// <summary>
/// Visual presentation only for the IT & IoT portfolio.
/// Prefer verified, repo-owned project cover/screenshot assets when a known
/// project title matches. Keep uploaded custom covers for other database
/// projects. Category artwork is an illustrative UI concept, not a screenshot
/// of a completed client product.
/// Does not modify Project or TeamMember database entities.
/// </summary>
public static class ItIotMedia
{
    public sealed record ProjectPreview(string Url, string Alt, bool IsIllustration);

    public static ProjectPreview ResolveProject(Project project)
    {
        ArgumentNullException.ThrowIfNull(project);
        var key = (project.Title + " " + project.Slug).ToLowerInvariant();

        // Real, locally stored artwork/screens belonging to the named project.
        if (Has(key, "saveit", "save-it", "save it"))
            return Real("/assets/projects/labib/saveit-screens.jpg", "SaveIt mobile application's interface screens");
        if (Has(key, "motoservis", "motoservice", "moto-servis", "moto service"))
            return Real("/assets/projects/labib/motointerface.png", "MotoServis mobile application service-booking screens");
        if (Has(key, "wargaku"))
            return Real("/assets/projects/labib/wargaku-screens.png", "Wargaku community services application interface screens");
        if (Has(key, "rasakita"))
            return Real("/assets/projects/labib/rasakita-screens.png", "Rasakita application interface screens");
        if (Has(key, "jute portal", "jute-portal"))
            return Real("/assets/projects/anik/jute-portal.jpg", "Jute Portal design case study cover artwork");
        if (Has(key, "crazydevs", "crazy devs"))
            return Real("/assets/projects/anik/crazydevs.jpg", "CrazyDevs design case study cover artwork");
        if (Has(key, "zenesen"))
            return Real("/assets/projects/anik/zenesen-cover.jpg", "Zenesen visual identity case study cover");
        if (Has(key, "luxury brand", "luxury-brand"))
            return Real("/assets/projects/anik/luxury-brand.jpg", "Luxury brand identity design artwork");
        if (Has(key, "jobfinder", "job finder", "job portal", "job-portal"))
            return Real("/images/ashik/jobfinder.png", "Job-finding software product interface screenshot");

        // A project manager's specific uploaded cover is authoritative for
        // projects not known above: never silently replace personal work.
        if (!string.IsNullOrWhiteSpace(project.HeroImage) &&
            !IsGenericPlaceholder(project.HeroImage))
            return new ProjectPreview(project.HeroImage, $"{project.Title} project cover", false);

        return ResolveConcept(project.Title, project.Slug, project.Category);
    }

    public static ProjectPreview ResolveConcept(string? title, string? slug, string? category)
    {
        var name = string.IsNullOrWhiteSpace(title) ? "Software project" : title.Trim();
        var key = (name + " " + slug + " " + category).ToLowerInvariant();
        var kind =
            Has(key, "ai ", "artificial intelligence", "knowledge", "chatbot", "machine learning", "llm") ? "ai"
            : Has(key, "iot", "sensor", "smart home", "embedded", "device", "monitoring") ? "iot"
            : Has(key, "security", "cyber", "soc", "firewall", "threat") ? "security"
            : Has(key, "hospital", "health", "medical", "clinic", "patient", "doctor", "pharma") ? "health"
            : Has(key, "school", "education", "student", "university", "campus", "learning") ? "education"
            : Has(key, "job", "recruit", "career", "hiring", "employee") ? "jobs"
            : Has(key, "shop", "ecommerce", "e-commerce", "store", "marketplace", "woocommerce") ? "commerce"
            : Has(key, "finance", "bank", "payment", "accounting", "expense", "billing", "pos") ? "finance"
            : Has(key, "inventory", "warehouse", "stock", "logistics", "supply chain") ? "inventory"
            : Has(key, "bike", "motorcycle", "moto", "vehicle", "garage", "transport") ? "automotive"
            : Has(key, "branding", "visual identity", "ux", "design", "graphic", "portfolio") ? "design"
            : Has(key, "mobile", "flutter", "android", "ios", "app") ? "mobile"
            : "software";

        return new ProjectPreview($"/assets/it-iot/previews/{kind}.svg",
            $"{name}: illustrative {kind} application preview", true);
    }

    private static ProjectPreview Real(string url, string alt) => new(url, alt, false);

    private static bool IsGenericPlaceholder(string url) =>
        Has(url.ToLowerInvariant(), "placeholder", "default-project", "no-image", "fallback");

    private static bool Has(string text, params string[] parts)
    {
        foreach (var p in parts)
            if (text.Contains(p, StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    /// <summary>
    /// Stable local illustrated avatar for every existing or newly created
    /// team member. These are illustrations, not claims of photographic likeness.
    /// Uploaded photographs remain stored and editable in the admin CMS.
    /// </summary>
    public static string AvatarFor(int memberId, string? name)
    {
        var seed = memberId > 0 ? memberId : StableHash(name ?? "");
        var index = Math.Abs((long)seed) % 12 + 1;
        return $"/assets/it-iot/avatars/avatar-{index:00}.svg";
    }

    private static int StableHash(string value)
    {
        unchecked
        {
            var result = 17;
            foreach (var c in value.ToUpperInvariant()) result = result * 31 + c;
            return result;
        }
    }
}
