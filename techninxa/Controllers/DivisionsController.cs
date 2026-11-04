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
            => Placeholder("Electrical Supply & Installation", "electrical");

        [HttpGet("/manpower-services")]
        public IActionResult ManpowerServices()
            => Placeholder("Manpower Services", "manpower");

        [HttpGet("/networking-services")]
        public IActionResult NetworkingServices()
            => Placeholder("Networking Services", "networking");

        private IActionResult Placeholder(string title, string division)
        {
            ViewData["Title"] = title;
            ViewData["Description"] = $"Cytech Engineering — {title}.";
            ViewData["ActiveDivision"] = division;
            return View("Placeholder");
        }
    }
}
