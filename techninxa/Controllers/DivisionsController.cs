using Microsoft.AspNetCore.Mvc;

namespace techninxa.Controllers
{
    /// <summary>
    /// Corporate service divisions. Placeholder pages are intentionally content-light
    /// until Cytech Engineering supplies the individual service details.
    /// </summary>
    public class DivisionsController : Controller
    {
        [HttpGet("/electrical-supply-installation")]
        public IActionResult ElectricalSupplyInstallation()
        {
            ViewData["Title"] = "Industrial Electrical Supply & Installation";
            ViewData["Description"] = "Cytech Engineering electrical distribution, power equipment, motor control and related installation services.";
            ViewData["ActiveDivision"] = "electrical";
            ViewData["IsCorporatePage"] = true;
            ViewData["CanonicalUrl"] = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            return View("ElectricalSupplyInstallation");
        }

        [HttpGet("/manpower-services")]
        public IActionResult ManpowerServices()
            => Placeholder("Manpower Services", "manpower");

        [HttpGet("/networking-services")]
        public IActionResult NetworkingServices()
        {
            ViewData["Title"] = "Networking Services";
            ViewData["Description"] = "Networking solutions for businesses: structured cabling, LAN and WAN, wireless, fiber, network equipment, security and support.";
            ViewData["ActiveDivision"] = "networking";
            ViewData["IsCorporatePage"] = true;
            ViewData["CanonicalUrl"] = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            return View("NetworkingServices");
        }

        private IActionResult Placeholder(string title, string division)
        {
            ViewData["Title"] = title;
            ViewData["Description"] = $"Cytech Engineering — {title}.";
            ViewData["ActiveDivision"] = division;
            return View("Placeholder");
        }
    }
}
