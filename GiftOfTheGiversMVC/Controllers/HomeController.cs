using GiftOfTheGiversMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GiftOfTheGiversMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // In-memory storage for contact messages
        private static List<ContactMessage> _messages = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitContact(string name, string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(message))
            {
                TempData["Error"] = "Please fill in all required fields.";
                return RedirectToAction("Contact");
            }

            _messages.Add(new ContactMessage
            {
                Id = _messages.Count + 1,
                Name = name,
                Email = email,
                Subject = string.IsNullOrWhiteSpace(subject) ? "(No Subject)" : subject,
                Message = message,
                SubmittedAt = DateTime.Now
            });

            TempData["Success"] = "Thank you for your message! We will get back to you soon.";
            return RedirectToAction("Contact");
        }

        public static List<ContactMessage> GetMessages() => _messages;

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult NotFoundPage()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }
    }
}