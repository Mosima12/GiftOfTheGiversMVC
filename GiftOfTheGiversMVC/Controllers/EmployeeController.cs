using GiftOfTheGiversMVC.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGiversMVC.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        // Sample data for the prototype (in-memory)
        private static List<ProjectUpdate> _projectUpdates = new()
        {
            new ProjectUpdate { Id = 1, Title = "Food parcels dispatched to KZN flood areas", Content = "500 food parcels dispatched to affected families in KwaZulu-Natal.", PostedBy = "admin@giftofthegivers.org", PostedDate = DateTime.Now.AddDays(-1) },
            new ProjectUpdate { Id = 2, Title = "Medical supplies delivered to Eastern Cape", Content = "Critical medical supplies delivered to 3 clinics in the Eastern Cape.", PostedBy = "admin@giftofthegivers.org", PostedDate = DateTime.Now.AddDays(-3) },
            new ProjectUpdate { Id = 3, Title = "New volunteer drive launched in Gauteng", Content = "Launched a volunteer recruitment drive in Gauteng this week.", PostedBy = "admin@giftofthegivers.org", PostedDate = DateTime.Now.AddDays(-5) }
        };

        // GET: /Employee/Dashboard
        public IActionResult Dashboard()
        {
            ViewBag.ProjectUpdates = _projectUpdates.OrderByDescending(p => p.PostedDate).ToList();

            // Get volunteer signups from VolunteerController
            var volunteers = VolunteerController.GetVolunteers();
            ViewBag.VolunteerSignups = volunteers;

            return View();
        }

        // POST: /Employee/PostUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostUpdate(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                TempData["Error"] = "Title and content are required.";
                return RedirectToAction("Dashboard");
            }

            var newUpdate = new ProjectUpdate
            {
                Id = _projectUpdates.Count + 1,
                Title = title,
                Content = content,
                PostedBy = User.Identity?.Name ?? "Unknown",
                PostedDate = DateTime.Now
            };

            _projectUpdates.Add(newUpdate);

            TempData["Message"] = $"Update '{title}' posted successfully!";
            return RedirectToAction("Dashboard");
        }
    }

    public class ProjectUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string PostedBy { get; set; } = string.Empty;
        public DateTime PostedDate { get; set; }
    }
}