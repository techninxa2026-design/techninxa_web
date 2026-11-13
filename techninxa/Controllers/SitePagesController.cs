using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using techninxa.Models;
using Techninxa;
using Techninxa.Models;
using Techninxa.Services;

namespace techninxa.Controllers
{
    /// <summary>
    /// Cytech Engineering corporate pages and inquiries. The original IT & IoT
    /// controller and its data flows remain independent.
    /// </summary>
    public class SitePagesController : Controller
    {
        private static readonly string[] Divisions =
        {
            "Electrical Supply & Installation",
            "Manpower Services",
            "Networking Services",
            "IT & IoT Services",
            "Supply of Industrial Products",
            "System Integration",
            "Installation & Commissioning",
            "Maintenance & Support",
            "Technical Consultation"
        };

        private readonly ApplicationDbContext _db;
        private readonly ILogger<SitePagesController> _logger;

        public SitePagesController(ApplicationDbContext db, ILogger<SitePagesController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Home()
        {
            SetPage("Home", "Electrical supply and installation, manpower, networking, and IT & IoT services.");
            return View("Home");
        }

        [HttpGet("/about")]
        public IActionResult About()
        {
            SetPage("About", "Learn about Cytech Engineering and its four technical service divisions.");
            return View("About");
        }

        [HttpGet("/contact")]
        public IActionResult Contact()
        {
            SetPage("Contact", "Contact Cytech Engineering about electrical, manpower, networking or IT & IoT requirements.");
            var requestedDivision = Request.Query["service"].ToString();
            var requestedProduct = CytechCatalogue.FindByTitle(Request.Query["product"].ToString());
            var model = new CorporateInquiryViewModel
            {
                ServiceDivision = Divisions.Contains(requestedDivision, StringComparer.Ordinal)
                    ? requestedDivision
                    : requestedProduct != null ? "Supply of Industrial Products" : string.Empty,
                InquiryType = requestedProduct != null ? "Request a quotation" : "Request a quotation",
                Details = requestedProduct != null
                    ? $"Please provide a quotation and availability for {requestedProduct.Title} (catalogue category {requestedProduct.Number:D2})."
                    : string.Empty
            };
            return View("Contact", model);
        }

        [HttpPost("/contact")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitInquiry(CorporateInquiryViewModel model)
        {
            SetPage("Contact", "Submit a corporate inquiry to Cytech Engineering.");

            if (!Divisions.Contains(model.ServiceDivision, StringComparer.Ordinal))
                ModelState.AddModelError(nameof(model.ServiceDivision), "Select a valid service division.");

            // Honeypot: silently ignore automated submissions.
            if (!string.IsNullOrWhiteSpace(model.Website))
                return RedirectToAction(nameof(Contact));

            if (!ModelState.IsValid)
                return View("Contact", model);

            var details = model.Details.Trim();
            var message =
                "[CYTECH CORPORATE INQUIRY]\n" +
                $"Division: {model.ServiceDivision}\n" +
                $"Inquiry: {model.InquiryType}\n" +
                $"Company: {model.Company?.Trim() ?? "Not specified"}\n" +
                $"Phone: {model.Phone.Trim()}\n\n" +
                details;

            if (message.Length > 2000)
            {
                ModelState.AddModelError(nameof(model.Details), "Please shorten your requirement details.");
                return View("Contact", model);
            }

            try
            {
                _db.ContactMessages.Add(new ContactMessage
                {
                    Name = model.Name.Trim(),
                    Email = model.Email.Trim(),
                    // Division details are kept in Message; do not mislabel web/software as IoT.
                    ProjectType = ProjectType.Other,
                    Message = message,
                    CreatedAt = DateTime.UtcNow
                });
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Unable to save Cytech corporate inquiry.");
                ModelState.AddModelError(string.Empty,
                    "The inquiry could not be saved right now. Please try again later.");
                return View("Contact", model);
            }

            TempData["CorporateInquirySuccess"] =
                "Thank you. Your inquiry has been received for review.";
            return RedirectToAction(nameof(Contact));
        }

        [HttpGet("/blog")]
        public IActionResult Blog()
        {
            SetPage("Blog", "Cytech Engineering blog.");
            return View("Placeholder");
        }

        [HttpGet("/products")]
        public IActionResult Products()
        {
            SetPage("Products", "Explore 38 product sections from the Cytech Engineering Industrial Product Catalogue 2026.");
            return View("Products", CytechCatalogue.Items);
        }

        [HttpGet("/products/{slug}")]
        public IActionResult ProductDetails(string slug)
        {
            var item = CytechCatalogue.Find(slug);
            if (item == null) return NotFound();
            SetPage(item.Title, $"{item.Description} Equipment categories from the Cytech Engineering 2026 catalogue.");
            ViewData["ActiveProductSection"] = item.Family;
            return View("ProductDetails", item);
        }

        [HttpGet("/services")]
        public IActionResult Services()
        {
            SetPage("Services", "Cytech Engineering supplies industrial products and provides system integration, installation, commissioning, maintenance, support and technical consultation.");
            return View("Services", CytechCatalogue.Services);
        }

        private void SetPage(string title, string description)
        {
            ViewData["Title"] = title;
            ViewData["Description"] = description;
            ViewData["ActiveDivision"] = "none";
            ViewData["IsCorporatePage"] =
                title != "Blog";
            ViewData["CanonicalUrl"] = $"{Request.Scheme}://{Request.Host}{Request.Path}";
        }
    }
}
