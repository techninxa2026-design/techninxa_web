using System;
using System.Collections.Generic;
using System.Linq;

namespace Techninxa.Services;

/// <summary>
/// Public website catalogue transcribed from "Cytech Engineering - Industrial Product Catalogue 2026".
/// Each numbered category corresponds to page Number + 1 in the supplied PDF.
/// This is a catalogue of product types, not a live inventory, price list or distributor authorization.
/// </summary>
public sealed record CatalogueCategory(
    int Number,
    string Title,
    string Family,
    string Description,
    string[] Products,
    string[] Brands)
{
    public int SourcePage => Number + 1;
    public string Slug => Title.ToLowerInvariant()
        .Replace(" & ", "-")
        .Replace(", ", "-")
        .Replace(" ", "-")
        .Replace("/", "-")
        .Replace("(", "")
        .Replace(")", "");
}

public sealed record CytechService(string Slug, string Name, string Summary);

public static class CytechCatalogue
{
    public const string BusinessName = "Cytech Engineering";
    public const string Email = "info@cytechbd.com";
    public const string Website = "www.cytechbd.com";
    public const string PhonePrimary = "+8801711015660";
    public const string PhoneSecondary = "+8801790379729";
    public const string Address = "BSCIC Electronic Complex, Level 6, Avenue 4, Road 3, Section 7, Mirpur, Dhaka, Bangladesh";

    public static readonly IReadOnlyList<CytechService> Services = new[]
    {
        new CytechService("industrial-products", "Supply of Industrial Products", "Industrial automation, instrumentation, electrical, networking, safety, security and power products."),
        new CytechService("system-integration", "System Integration", "Integration of industrial control, connectivity and related technical systems."),
        new CytechService("installation-commissioning", "Installation & Commissioning", "Installation and commissioning for industrial and technical systems."),
        new CytechService("maintenance-support", "Maintenance & Support", "Technical maintenance, product support and after-sales assistance."),
        new CytechService("technical-consultation", "Technical Consultation", "Consultation to identify suitable equipment and engineering approaches.")
    };

    public static readonly IReadOnlyList<string> Industries = new[]
    {
        "Manufacturing", "Food & Beverage", "Pharmaceuticals", "Oil & Gas",
        "Power & Energy", "Water & Wastewater", "Building & Infrastructure"
    };

    public static readonly IReadOnlyList<CatalogueCategory> Items = new[]
    {
        new CatalogueCategory(1, "Industrial Networking", "Networking & Connectivity", "Reliable industrial data communication in demanding environments.", new[] { "Industrial Ethernet switches", "Managed and unmanaged switches", "Fiber media converters", "Industrial routers" }, new[] { "Siemens", "Schneider Electric", "MOXA", "Teltonika" }),
        new CatalogueCategory(2, "Industrial Instrumentation", "Automation & Instrumentation", "Instruments and sensors for process measurement and monitoring.", new[] { "Pressure sensors", "Temperature sensors", "Flow meters", "Level sensors", "Transmitters" }, Array.Empty<string>()),
        new CatalogueCategory(3, "Industrial Automation", "Automation & Instrumentation", "Automation components for machine and process control.", new[] { "PLC systems", "HMI displays", "Variable frequency drives", "Servo motors & drives", "Industrial sensors" }, new[] { "Siemens", "ABB", "Schneider Electric", "SICK" }),
        new CatalogueCategory(4, "Electrical Distribution", "Electrical & Power", "Industrial electrical distribution and protection equipment.", new[] { "MCCBs", "MCBs", "Contactors", "Overload relays", "Air circuit breakers", "Distribution panels" }, new[] { "Schneider Electric", "ABB", "Siemens" }),
        new CatalogueCategory(5, "Industrial Safety & Security", "Safety & Security", "Detection, emergency and monitoring equipment for facilities.", new[] { "Fire alarm systems", "Smoke detectors", "Beam detectors", "Emergency exit systems", "CCTV surveillance", "Access control" }, new[] { "Honeywell", "Schneider Electric", "Hikvision", "Dahua" }),
        new CatalogueCategory(6, "Power & Backup Solutions", "Electrical & Power", "Power protection and continuity components for operations.", new[] { "UPS systems", "Battery banks", "Automatic voltage regulators", "Solar inverters", "Power supplies" }, new[] { "ABB", "Schneider Electric", "APC" }),
        new CatalogueCategory(7, "Patch Cords", "Networking & Connectivity", "Copper and fiber patch connections for network infrastructure.", new[] { "CAT6 UTP patch cord", "CAT6A UTP patch cord", "CAT6A STP patch cord", "Fiber optic patch cord", "CAT7 SSTP patch cord" }, Array.Empty<string>()),
        new CatalogueCategory(8, "Structured Cabling Solutions", "Networking & Connectivity", "Physical network infrastructure for dependable connectivity.", new[] { "Cat5e/Cat6/Cat6A/Cat7 copper cabling", "Singlemode/multimode fiber cabling", "Network racks", "Cable management", "Network testing tools" }, Array.Empty<string>()),
        new CatalogueCategory(9, "Faceplates & Surface Boxes", "Networking & Connectivity", "Outlet and wall termination accessories.", new[] { "1/2/4-port faceplates", "Surface mount boxes", "Back boxes" }, Array.Empty<string>()),
        new CatalogueCategory(10, "Industrial Plug & Sockets", "Electrical & Power", "Industrial connections for demanding power environments.", new[] { "IEC 60309 plugs", "IEC 60309 sockets", "Mobile connectors", "Appliance inlets" }, Array.Empty<string>()),
        new CatalogueCategory(11, "Switches & Isolators", "Electrical & Power", "Manual switching and disconnection equipment.", new[] { "Cam switches", "Rotary isolators", "Changeover switches", "Load break switches" }, Array.Empty<string>()),
        new CatalogueCategory(12, "Modular Jacks (Keystone Jacks)", "Networking & Connectivity", "Modular copper network termination components.", new[] { "CAT3 keystone jacks", "CAT5E keystone jacks", "CAT6A keystone jacks", "Shielded CAT6A keystone jacks", "CAT7 keystone jacks" }, Array.Empty<string>()),
        new CatalogueCategory(13, "Connectors & Plugs", "Networking & Connectivity", "Copper and fiber connection accessories.", new[] { "RJ45 plugs", "RJ45 boots", "RJ45 inline couplers", "Keystone couplers", "Fiber connectors" }, Array.Empty<string>()),
        new CatalogueCategory(14, "Terminal Blocks & Connectors", "Electrical & Power", "Industrial wiring termination and distribution components.", new[] { "Screw terminal blocks", "Spring terminal blocks", "Distribution blocks", "Ferrules" }, Array.Empty<string>()),
        new CatalogueCategory(15, "Cable Management Solutions", "Industrial Infrastructure", "Cable routing, protection and identification products.", new[] { "Cable trays", "Cable ducts", "Cable ties", "Cable markers" }, Array.Empty<string>()),
        new CatalogueCategory(16, "Cable Glands & Accessories", "Industrial Infrastructure", "Cable entry, retention and protection components.", new[] { "Nylon cable glands", "Brass cable glands", "Metal cable glands", "Spiral cable wrap" }, Array.Empty<string>()),
        new CatalogueCategory(17, "Circuit Breakers & Protection Devices", "Electrical & Power", "Circuit interruption, isolation and protective switching products.", new[] { "MCBs", "MCCBs", "ACBs", "RCCBs/RCBOs", "Isolators & switches" }, new[] { "ABB", "Schneider Electric", "Siemens", "CHINT" }),
        new CatalogueCategory(18, "Networking Accessories", "Networking & Connectivity", "Cabling connectors, termination accessories and network testing tools.", new[] { "RJ45 connectors", "Keystone jacks", "Faceplates", "Patch cords", "Cable management", "Tools & testers" }, new[] { "UGREEN", "D-Link", "Panduit", "Shenzhen" }),
        new CatalogueCategory(19, "Industrial Ethernet Switches", "Networking & Connectivity", "Managed and unmanaged industrial Ethernet switching.", new[] { "Fast Ethernet/Gigabit/10GbE options", "PoE/PoE+/PoE++ options", "DIN rail mounting", "Redundant power inputs" }, new[] { "MOXA", "D-Link", "Panduit", "Shenzhen" }),
        new CatalogueCategory(20, "PLC, HMI & SCADA Solutions", "Automation & Instrumentation", "Industrial controllers, operator interfaces and supervisory monitoring software.", new[] { "PLC controllers", "HMI displays", "SCADA software" }, new[] { "Siemens", "ABB", "Schneider Electric", "Delta", "Weintek" }),
        new CatalogueCategory(21, "VFD & Servo Systems", "Automation & Instrumentation", "Variable-frequency drives and precision motion control.", new[] { "AC drives", "Servo drives", "Servo motors", "Motion control components" }, new[] { "ABB", "Schneider Electric", "Delta", "INVT" }),
        new CatalogueCategory(22, "Industrial Sensors", "Automation & Instrumentation", "Sensor technologies for monitoring, automation and safety.", new[] { "Proximity sensors", "Photoelectric sensors", "Pressure sensors", "Temperature sensors", "Level sensors", "Flow sensors" }, new[] { "SICK", "OMRON", "Autonics", "Pepperl+Fuchs" }),
        new CatalogueCategory(23, "CCTV & Surveillance Systems", "Safety & Security", "Video surveillance equipment for industrial and commercial sites.", new[] { "IP cameras", "PTZ cameras", "Network video recorders", "Video management software", "Accessories" }, new[] { "Hikvision", "Dahua", "UNV" }),
        new CatalogueCategory(24, "Access Control Solutions", "Safety & Security", "Electronic access management and door security components.", new[] { "Access controllers", "Card/RFID readers", "Biometric devices", "Electromagnetic locks", "Exit switches & accessories" }, new[] { "ZKTeco", "Hikvision", "Suprema", "Dahua" }),
        new CatalogueCategory(25, "Fire Alarm Systems", "Safety & Security", "Fire detection and alarm control equipment.", new[] { "Fire alarm control panels", "Smoke detectors", "Heat detectors", "Manual call points", "Sounders & beacons", "Accessories" }, new[] { "Hochiki", "Notifier by Honeywell", "GST", "Apollo" }),
        new CatalogueCategory(26, "Patch Panels & Accessories", "Networking & Connectivity", "Structured cabling termination and distribution hardware.", new[] { "Cat5e/Cat6/Cat6A/Cat7 panels", "24/48-port patch panels", "Keystone punch-downs", "Cable managers" }, new[] { "Panduit", "D-Link", "UGREEN" }),
        new CatalogueCategory(27, "Fiber Optic Cables & ODF", "Networking & Connectivity", "Fiber backbone cable and optical distribution components.", new[] { "Singlemode/multimode cable", "Indoor/outdoor fiber cable", "ODF racks & boxes", "Fiber pigtails & patch cords", "Adapters & accessories" }, new[] { "Shenzhen", "Panduit", "D-Link" }),
        new CatalogueCategory(28, "Industrial UPS Systems", "Electrical & Power", "Uninterruptible power systems for critical equipment.", new[] { "Online/double conversion UPS", "Rack/tower/modular units", "Battery management", "SNMP/monitoring options" }, new[] { "ABB", "CyberPower", "APC", "Delta" }),
        new CatalogueCategory(29, "Power Supplies & SMPS", "Electrical & Power", "Industrial DC conversion and power supply units.", new[] { "DIN rail power supplies", "Industrial SMPS", "DC-DC converters", "Redundant power supplies" }, new[] { "Mean Well", "Delta", "Schneider Electric", "ABB" }),
        new CatalogueCategory(30, "MCC Panels & Control Centers", "Electrical & Power", "Power distribution and motor control panels.", new[] { "MCC panels", "PCC panels", "APFC panels", "Distribution boards", "Custom-built solutions" }, new[] { "ABB", "Schneider Electric", "Siemens", "Eaton" }),
        new CatalogueCategory(31, "Cable Trays & Cable Ladders", "Industrial Infrastructure", "Support systems for power and control cables.", new[] { "GI/powder-coated cable trays", "Cable ladders", "Raceways & accessories", "Hangers, supports & fittings" }, new[] { "Panduit", "ABB", "Legrand", "KATKO" }),
        new CatalogueCategory(32, "Earthing & Lightning Protection", "Industrial Infrastructure", "Electrical grounding and surge/lightning protection products.", new[] { "Copper bonded rods", "GI earthing electrodes", "Earthing strips & clamps", "Lightning arrestors (SPD)", "Earthing accessories" }, new[] { "KATKO", "ABB", "Schneider Electric", "Legrand" }),
        new CatalogueCategory(33, "Industrial Lighting Fixtures", "Industrial Infrastructure", "Lighting for industrial areas.", new[] { "LED high bay lights", "LED flood lights", "LED tube/batten lights", "Explosion-proof lights", "Bulkhead & wall glass fittings" }, new[] { "Philips", "Schneider Electric", "Chalmit", "Rosenberg" }),
        new CatalogueCategory(34, "Industrial Fans & Ventilation", "Industrial Infrastructure", "Industrial air movement and ventilation components.", new[] { "Axial fans", "Centrifugal fans", "Roof extractors", "Ventilation systems", "Accessories" }, new[] { "Rosenberg", "Systemair", "Ventis", "KATKO" }),
        new CatalogueCategory(35, "Junction Boxes & Enclosures", "Industrial Infrastructure", "Protective housings for industrial connections and control.", new[] { "GI/stainless steel enclosures", "IP65/IP66 junction boxes", "Control boxes", "Terminal boxes", "Custom enclosures" }, new[] { "Schneider Electric", "ABB", "KATKO", "Rosenberg" }),
        new CatalogueCategory(36, "Push Buttons & Pilot Lamps", "Automation & Instrumentation", "Industrial operator controls and indication devices.", new[] { "Push buttons", "Pilot lamps", "Selector switches", "Emergency stop", "Control stations" }, new[] { "Schneider Electric", "ABB", "Siemens", "IDEC" }),
        new CatalogueCategory(37, "Relays & Timers", "Automation & Instrumentation", "Relay and timing components for industrial control.", new[] { "Intermediate relays", "Power relays", "Time delay relays", "Lightning relays", "Socket bases" }, new[] { "Schneider Electric", "ABB", "OMRON", "Siemens" }),
        new CatalogueCategory(38, "Contactors & Overload Relays", "Electrical & Power", "Motor switching and protection components.", new[] { "AC/DC contactors", "Overload relays", "Thermal relays", "Auxiliary contact blocks", "Accessories" }, new[] { "ABB", "Schneider Electric", "Siemens", "CHINT" })
    };

    public static readonly IReadOnlyList<string> Families = new[]
    {
        "Networking & Connectivity",
        "Automation & Instrumentation",
        "Electrical & Power",
        "Safety & Security",
        "Industrial Infrastructure"
    };

    public static CatalogueCategory? Find(string? slug) =>
        Items.FirstOrDefault(item => string.Equals(item.Slug, slug, StringComparison.OrdinalIgnoreCase));

    public static CatalogueCategory? FindByTitle(string? title) =>
        Items.FirstOrDefault(item => string.Equals(item.Title, title, StringComparison.OrdinalIgnoreCase));
}
