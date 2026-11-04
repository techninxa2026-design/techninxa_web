using Microsoft.AspNetCore.Mvc;

namespace techninxa.Controllers
{
    /// <summary>
    /// Corporate pages that will be populated when Cytech's content is ready.
    /// This controller does not access or alter existing IT & IoT data.
    /// </summary>
    public class SitePagesController : Controller
    {
        [HttpGet("/blog")]
        public IActionResult Blog()
        {
            ViewData["Title"] = "Blog";
            ViewData["Description"] = "Cytech Engineering blog.";
            ViewData["ActiveDivision"] = "none";
            return View("Placeholder");
        }

        [HttpGet("/products")]
        public IActionResult Products()
        {
            ViewData["Title"] = "Products";
            ViewData["Description"] = "Cytech Engineering products.";
            ViewData["ActiveDivision"] = "none";
            return View("Placeholder");
        }
    }
}
