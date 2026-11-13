namespace Techninxa.Services;

/// <summary>
/// Editorial imagery for the corporate site. All photographic images below are
/// illustrative industry photographs licensed for free use by their contributors
/// on Unsplash, NOT photographs of projects performed by Cytech Engineering.
/// Catalogue categories, services and specifications remain sourced from the
/// supplied Cytech Engineering Industrial Product Catalogue 2026.
/// </summary>
public static class CytechVisuals
{
    public const string Hero = "https://images.unsplash.com/photo-1726111262949-e22631a8c376?auto=format&fit=crop&w=2000&q=84";
    public const string IntegrationDetail = "https://images.unsplash.com/photo-1780034766228-3fd70d9463c3?auto=format&fit=crop&w=1200&q=83";
    public const string WiringDetail = "https://images.unsplash.com/photo-1544724569-5f546fd6f2b5?auto=format&fit=crop&w=1200&q=82";

    public static string ServiceImage(string? slug) => slug switch
    {
        "industrial-products" => "https://images.unsplash.com/photo-1780034766228-3fd70d9463c3?auto=format&fit=crop&w=880&q=82",
        "system-integration" => "https://images.unsplash.com/photo-1775519520461-6b6e068d9250?auto=format&fit=crop&w=880&q=82",
        "installation-commissioning" => "https://images.unsplash.com/photo-1783419538460-676cb89a0d19?auto=format&fit=crop&w=880&q=82",
        "maintenance-support" => "https://images.unsplash.com/photo-1748347568194-c8cd8edd27da?auto=format&fit=crop&w=880&q=82",
        "technical-consultation" => "https://images.unsplash.com/photo-1769149068959-b11392164add?auto=format&fit=crop&w=880&q=82",
        _ => IntegrationDetail
    };

    public static string ServiceAlt(string? slug) => slug switch
    {
        "industrial-products" => "Industrial electrical and control equipment inside a cabinet",
        "system-integration" => "Industrial electronic components arranged in control racks",
        "installation-commissioning" => "Technician installing electronics inside a control enclosure",
        "maintenance-support" => "Engineer working on industrial processing machinery",
        "technical-consultation" => "Engineer reviewing an electronic circuit design at a computer",
        _ => "Industrial engineering equipment"
    };

    public static string FamilyImage(string? family) => family switch
    {
        "Networking & Connectivity" => "https://images.unsplash.com/photo-1594915440248-1e419eba6611?auto=format&fit=crop&w=980&q=82",
        "Automation & Instrumentation" => "https://images.unsplash.com/photo-1780034766228-3fd70d9463c3?auto=format&fit=crop&w=980&q=82",
        "Electrical & Power" => "https://images.unsplash.com/photo-1544724569-5f546fd6f2b5?auto=format&fit=crop&w=980&q=82",
        "Safety & Security" => "https://images.unsplash.com/photo-1758514475000-f7d515078c81?auto=format&fit=crop&w=980&q=82",
        "Industrial Infrastructure" => "https://images.unsplash.com/photo-1726111262949-e22631a8c376?auto=format&fit=crop&w=980&q=82",
        _ => IntegrationDetail
    };

    public static string FamilyAlt(string? family) => family switch
    {
        "Networking & Connectivity" => "Fiber-optic patch cables in a network equipment rack",
        "Automation & Instrumentation" => "Industrial control panel and instrumentation components",
        "Electrical & Power" => "Electrical distribution panel and circuit breakers",
        "Safety & Security" => "Security camera mounted near a building",
        "Industrial Infrastructure" => "Industrial facility with processing equipment and pipework",
        _ => "Industrial engineering equipment"
    };

    public static string FeatureImage(int categoryNumber) => categoryNumber switch
    {
        3 => "https://images.unsplash.com/photo-1780034766228-3fd70d9463c3?auto=format&fit=crop&w=980&q=82",
        19 => "https://images.unsplash.com/photo-1594915440248-1e419eba6611?auto=format&fit=crop&w=980&q=82",
        20 => "https://images.unsplash.com/photo-1748256223955-5d5eb7fe7893?auto=format&fit=crop&w=980&q=82",
        23 => "https://images.unsplash.com/photo-1758514475000-f7d515078c81?auto=format&fit=crop&w=980&q=82",
        28 => "https://images.unsplash.com/photo-1775519520461-6b6e068d9250?auto=format&fit=crop&w=980&q=82",
        30 => "https://images.unsplash.com/photo-1544724569-5f546fd6f2b5?auto=format&fit=crop&w=980&q=82",
        _ => IntegrationDetail
    };
}
