using Microsoft.AspNetCore.Mvc;

namespace techninxa.Controllers
{
    /// <summary>
    /// Company-wide pages are intentionally left as branded shells until their
    /// individual content is approved. They do not query the IT & IoT database.
    /// </summary>
    public class SitePagesController : Controller
    {
        public IActionResult Home() => ShowPlaceholder("Home");

        [HttpGet("/about")]
        public IActionResult About() => ShowPlaceholder("About");

        [HttpGet("/contact")]
        public IActionResult Contact() => ShowPlaceholder("Contact");

        [HttpGet("/blog")]
        public IActionResult Blog() => ShowPlaceholder("Blog");

        [HttpGet("/products")]
        public IActionResult Products() => ShowPlaceholder("Products");

        private IActionResult ShowPlaceholder(string title)
        {
            ViewData["Title"] = title;
            ViewData["Description"] = $"Cytech Engineering — {title}.";
            ViewData["ActiveDivision"] = "none";
            ViewData["CanonicalUrl"] = $"{Request.Scheme}://{Request.Host}{Request.Path}";
            return View("Placeholder");
        }
    }
}
