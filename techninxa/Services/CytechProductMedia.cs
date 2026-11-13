using System;
using System.Collections.Generic;

namespace Techninxa.Services;

/// <summary>
/// Representative product photographs matching the 38 numbered industrial catalogue sections.
/// Image URLs and attribution link to their original publicly available publishers.
/// These pictures are visual examples, not Cytech stock photos, product listings or proof of
/// a dealership. Confirm reuse rights or replace with approved Cytech product photography
/// before production deployment.
/// </summary>
public sealed record CytechProductPhoto(string Url, string Alt, string Source, string SourcePage);

public static class CytechProductMedia
{
    private static readonly IReadOnlyDictionary<int, CytechProductPhoto> Photos =
        new Dictionary<int, CytechProductPhoto>
        {
            [1] = new CytechProductPhoto("https://cdn-cms-frontdoor-dfc8ebanh6bkb3hs.a02.azurefd.net/en/getattachment/Products/INDUSTRIAL-NETWORK-INFRASTRUCTURE/Ethernet-Switches/Layer-2-Managed-Switches/EDS-510A-Series/moxa-eds-510a-series-image-1-%282%29.jpg", "Industrial Ethernet switches with RJ45 and fiber ports", "Moxa", "https://www.moxa.com/en/products/industrial-network-infrastructure/ethernet-switches/layer-2-managed-switches/eds-510a-series"),
            [2] = new CytechProductPhoto("https://www.emerson.com/is/image/emerson/dl-rmt-3051-pressure-trans-fam-2?fmt=webp-alpha&qlt=95", "Pressure and process instrumentation transmitters", "Emerson", "https://www.emerson.com/en/measurement-instrumentation/products/rosemount-3051-pressure-transmitter-family"),
            [3] = new CytechProductPhoto("https://www.electro-matic.com/wp-content/uploads/2020/11/contact_support-products.png", "Industrial PLC, HMI and drive control components", "Electro-Matic", "https://www.electro-matic.com/contact/"),
            [4] = new CytechProductPhoto("https://images.wiautomation.com/public/images/landing/anticipa/product/1SDA054101R1.jpg", "Molded case circuit breakers for electrical distribution", "WIAutomation", "https://us.wiautomation.com/abb/drives-motors-circuits-protection/1SDA054101R1"),
            [5] = new CytechProductPhoto("https://www.trimatrik.net/public/uploads/category/1728212106-cctv-ip-camera-solutions-company-bangladesh.webp", "Fire alarm, surveillance and access control security equipment", "Trimatrik", "https://trimatrik.net/"),
            [6] = new CytechProductPhoto("https://gfx3.senetic.com/akeneo-catalog/4/1/1/2/4112f3fac6cc381cdce01ff7ab5021186987fcf5_1728614_E3SUPS15K3IB1_image1.jpg", "Industrial UPS tower backup power system", "Senetic", "https://www.senetic.co.ke/product/E3SUPS15K3IB1"),
            [7] = new CytechProductPhoto("https://media.falabella.com/falabellaCO/154744562_03/w%3D1500%2Ch%3D1500%2Cfit%3Dcover", "Blue CAT6 RJ45 network patch cable", "Falabella", "https://www.falabella.com.co/falabella-co/product/154744561/cable-de-red-patch-cord-cat6-u-utp-24awg-azul-1-metro-rj45/154744562"),
            [8] = new CytechProductPhoto("https://static.wixstatic.com/media/3adbca_ced48f500ae8443b9c443e2e8a9b36db~mv2.png/v1/fill/w_654%2Ch_524%2Cal_c%2Clg_1%2Cq_85%2Cenc_avif%2Cquality_auto/3adbca_ced48f500ae8443b9c443e2e8a9b36db~mv2.png", "Structured cabling rack with patch panels and network cables", "ACE Based", "https://www.acebased.com/server-rack-installation"),
            [9] = new CytechProductPhoto("https://www.bestlinknetware.com/products/101922WT-3.jpg", "Two-port CAT6 surface mount network outlet box", "Bestlink Netware", "https://www.bestlinknetware.com/Product/101922WT"),
            [10] = new CytechProductPhoto("https://image.made-in-china.com/203f0j00phfuJSVBLycb/IEC60309-Standard-IP44-IP67-Water-Proof-16A-32A-63A-125A-Electric-Power-Supply-Industrial-Plug-and-Socket.webp", "Red and blue industrial IEC 60309 plugs and sockets", "Made-in-China", "https://sanunuo-elec.en.made-in-china.com/product/hZcfrTzKOEWt/China-IEC60309-Standard-IP44-IP67-Water-Proof-16A-32A-63A-125A-Electric-Power-Supply-Industrial-Plug-and-Socket.html"),
            [11] = new CytechProductPhoto("https://res.cloudinary.com/rsc/image/upload/b_rgb%3AFFFFFF%2Cc_pad%2Cdpr_2.625%2Cf_auto%2Ch_214%2Cq_auto%2Cw_380/c_pad%2Ch_214%2Cw_380/Y2736468-01?pgw=1", "Red and yellow rotary cam isolation switch", "RS", "https://no.rs-online.com/web/p/cam-switches/2736468"),
            [12] = new CytechProductPhoto("https://intellinetnetwork.de/cdn/shop/files/cat6a-10g-shielded-toolless-slim-keystone-jack-white-791304-1.jpg?v=1729787269", "Shielded CAT6A RJ45 modular keystone jack", "Intellinet", "https://intellinetnetwork.de/products/intellinet-de-cat6a-10g-slimline-keystone-modul-geschirmt-und-werkzeuglos-weiss-791304"),
            [13] = new CytechProductPhoto("https://cdn.rc-static.com/images/ProductImages/ScaleProducts/7564A887_Icat_5.jpg", "Ethernet CAT6 RJ45 coupler and patch cable connectors", "Redcorp", "https://www.redcorp.com/en/product/converters-adaptors/startech-com/coupler-inline-rj45-cat6-coupler-female-to-female-f-f-t568b-connector-unshielded-ethernet-cable-extension-in-cat6-coupler-u1/7564a887"),
            [14] = new CytechProductPhoto("https://www.wonkecontact.com/Content/upload/2018367379/201807271432342886042.jpg", "Gray DIN rail industrial spring terminal block", "Wonke Electric", "https://www.wonkecontact.com/spring-terminal-blocks/universal-terminal-blocks/din-rail-spring-terminal-blocks.html"),
            [15] = new CytechProductPhoto("https://image.made-in-china.com/2f0j00EqTkcWKBhzbo/Cable-Accessories-Cable-Raceway-Slotted-Duct.jpg", "Industrial slotted cable management duct and raceway", "Made-in-China", "https://image.made-in-china.com/2f0j00EqTkcWKBhzbo/Cable-Accessories-Cable-Raceway-Slotted-Duct.jpg"),
            [16] = new CytechProductPhoto("https://www.wiska.com/pic/upload/1_pic5ae1d71c29a3d.jpg", "Brass, nylon and metal cable glands and fittings", "WISKA", "https://www.wiska.com/en/30/cat/1917/cable-glands/"),
            [17] = new CytechProductPhoto("https://images.wiautomation.com/public/images/landing/anticipa/product/1SDA054101R1.jpg", "Industrial circuit breakers and protection devices", "WIAutomation", "https://us.wiautomation.com/abb/drives-motors-circuits-protection/1SDA054101R1"),
            [18] = new CytechProductPhoto("https://images.thdstatic.com/productImages/52925dce-3220-4acc-9126-fc4dde86f317/svn/klein-tools-electricians-tool-sets-80093-64_1000.jpg", "Network cable tester and RJ45 termination tools", "The Home Depot", "https://www.homedepot.com/p/321102998"),
            [19] = new CytechProductPhoto("https://cdn-cms-frontdoor-dfc8ebanh6bkb3hs.a02.azurefd.net/en/getattachment/Products/INDUSTRIAL-NETWORK-INFRASTRUCTURE/Ethernet-Switches/Layer-2-Managed-Switches/EDS-510A-Series/moxa-eds-510a-series-image-1-%282%29.jpg", "MOXA industrial managed Ethernet switches", "Moxa", "https://www.moxa.com/en/products/industrial-network-infrastructure/ethernet-switches/layer-2-managed-switches/eds-510a-series"),
            [20] = new CytechProductPhoto("https://s.alicdn.com/%40sc04/kf/H809201f91fcc4be99f0a0bfd3931532cF/100-New-Original-Siemens-6SL3244-0BB13-1FA0-SINAMICS-G120-CU240E-2-PN-F-Control-Unit-E-type-6SL32440BB131FA0.jpg", "Industrial PLC and HMI operator interface hardware", "Alibaba", "https://www.alibaba.com/showroom/siemens-g120-sinamics.html"),
            [21] = new CytechProductPhoto("https://www.wistexllc.com/product_images/uploaded_images/abb-acs580.jpg", "ABB industrial variable frequency drives", "Wistex", "https://www.wistexllc.com/blog/abb-acs580-energy-management-facilities/"),
            [22] = new CytechProductPhoto("https://www.libertyelectricproducts.com/images/sensing-automation.jpg", "Industrial photoelectric and proximity sensors", "Liberty Electric", "https://www.libertyelectricproducts.com/sensors-controls-safety/"),
            [23] = new CytechProductPhoto("https://www.essentialsecuritysupplies.co.nz/cdn/shop/collections/Dahua_Ip_surveillance_kitsets_security_Surveillance_Camera_Alliance_Wholesale.png?v=1648947378&width=2000", "Dahua CCTV surveillance cameras and network video recorder", "Essential Security Supplies", "https://www.essentialsecuritysupplies.co.nz/collections/dahua-ip-kits"),
            [24] = new CytechProductPhoto("https://zktecoua.com/files/tf1700.png", "Fingerprint and keypad biometric access control terminal", "ZKTeco", "https://zktecoua.com/en/products/fingerprint/tf1700.htm"),
            [25] = new CytechProductPhoto("https://crowngasqatar.com/wp-content/uploads/2023/09/Crowngas-Apollo-Fire-Alarm-System-e1700466567141.jpg", "Apollo fire alarm control panels and smoke detectors", "Crowngas", "https://crowngasqatar.com/product/apollo-fire-alarm-system/"),
            [26] = new CytechProductPhoto("https://icc.com/wp-content/uploads/2019/08/cat6-patch-panel-24-ports-1-rms-icmpp02460-1000-600x600.jpg", "Twenty-four-port CAT6 network patch panel", "ICC", "https://icc.com/product/cat6-patch-panel-24-ports-1-rms/"),
            [27] = new CytechProductPhoto("https://images.satu.kz/230309363_w1280_h640_230309363.jpg", "Fiber optic distribution frame with SC connectors", "Satu / Netkom", "https://netkom.kz/p122326761-opticheskij-kross-odf.html"),
            [28] = new CytechProductPhoto("https://gfx3.senetic.com/akeneo-catalog/4/1/1/2/4112f3fac6cc381cdce01ff7ab5021186987fcf5_1728614_E3SUPS15K3IB1_image1.jpg", "APC industrial uninterruptible power system", "Senetic", "https://www.senetic.co.ke/product/E3SUPS15K3IB1"),
            [29] = new CytechProductPhoto("https://www.meanwell.co.uk/assets/images/products/_738x738_letterbox_center-center_none/7116/SDR-75.webp", "Industrial DIN-rail switching power supply", "Mean Well", "https://www.meanwell.co.uk/power-supplies/din-rail-power-supplies/sdr-75-series"),
            [30] = new CytechProductPhoto("https://www.harshautomation.com/seo-images/mcc-panel-manufacturers.jpg", "Industrial motor control center electrical panel lineup", "Harsh Automation", "https://www.harshautomation.com/mcc-panel-manufacturers-in-india.php"),
            [31] = new CytechProductPhoto("https://image.made-in-china.com/2f0j00ykMVBcqlwdfe/Hot-DIP-Galvanized-Cable-Tray-High-Strength-Cable-Ladder-Price-with-Best-Quality.webp", "Galvanized steel cable trays and cable ladders", "Made-in-China", "https://emtconduitpipe.en.made-in-china.com/product/wdpARJVgqztG/China-Hot-DIP-Galvanized-Cable-Tray-High-Strength-Cable-Ladder-Price-with-Best-Quality.html"),
            [32] = new CytechProductPhoto("https://www.solucaomateriaiseletricos.com.br/spda-produtos.png", "Copper earthing electrodes and surge protection equipment", "Solução Materiais Elétricos", "https://www.solucaomateriaiseletricos.com.br/materiais.html"),
            [33] = new CytechProductPhoto("https://www.agcled.com/static/products/hb37/HB37-LED-High-bay-Flood-Light.jpg", "Industrial LED high-bay lighting fixture", "AGC Lighting", "https://www.agcled.com/solutions/"),
            [34] = new CytechProductPhoto("https://img.archiexpo.com/images_ae/photo-g/60815-13960895.jpg", "Industrial centrifugal fan for ventilation systems", "Archiexpo", "https://www.archiexpo.com/prod/rosenberg-ventilatoren/product-60815-1751600.html"),
            [35] = new CytechProductPhoto("https://assets.cef.co.uk/images/pdg/tele_nsytbs19168_b/original/tele_nsytbs19168_b.jpg", "Weather-resistant electrical junction enclosure", "CEF", "https://www.cef.co.uk/catalogue/products/1868485-192mmx164mmx87mm-abs-enclosure-ip66"),
            [36] = new CytechProductPhoto("https://www.electriccasa.ro/images/detailed/118/xb4bw73731g5-schneider1.jpg", "Green and red industrial start-stop push button", "Electric Casa", "https://www.electriccasa.ro/buton-comanda-schneider-start-stop-cu-revenire-si-led-1ni1nd-alb-xb4bw73731g5/"),
            [37] = new CytechProductPhoto("https://static-data2.manualslib.com/product-images/573/2161505/omron-h3cr-timer.jpg", "Omron industrial time-delay relay timer", "ManualsLib", "https://www.manualslib.com/manual/2161505/Omron-H3cr.html"),
            [38] = new CytechProductPhoto("https://www.hgr-ag.ch/fileadmin/user_upload/hgr-3polige-af-schuetze.jpg", "ABB industrial contactors and motor protection devices", "HGR", "https://www.hgr-ag.ch/produits/proteger-enclencher-commander-controler/niederspannungs-schaltgeraete/schuetze-motorschutzschalter/3-polige-schuetze-und-ueberlastrelais-fuer-motorstart/?L=1"),
        };

    public static CytechProductPhoto For(int number) =>
        Photos.TryGetValue(number, out var image)
            ? image
            : throw new ArgumentOutOfRangeException(nameof(number), "No catalogue photo found.");

    public static bool HasImage(int number) => Photos.ContainsKey(number);

    public static int ImageCount => Photos.Count;
}
