using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGiversMVC.Controllers
{
    public class VolunteerController : Controller
    {
        // In-memory storage for the prototype
        private static List<VolunteerRegistration> _volunteers = new();

        // GET: /Volunteer/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Volunteer/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(VolunteerRegistration volunteer, string[] skills)
        {
            if (skills != null && skills.Length > 0)
            {
                volunteer.Skills = skills.ToList();
            }

            volunteer.Id = _volunteers.Count + 1;
            volunteer.RegistrationDate = DateTime.Now;
            _volunteers.Add(volunteer);

            TempData["Message"] = "Thank you for registering as a volunteer! We will contact you soon.";
            return RedirectToAction("Confirmation");
        }

        // GET: /Volunteer/Confirmation
        public IActionResult Confirmation()
        {
            return View();
        }

        // Static method for Employee dashboard to view volunteers
        public static List<VolunteerRegistration> GetVolunteers() => _volunteers;
    }

    public class VolunteerRegistration
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new();
        public string Availability { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
    }
}