using GiftOfTheGiversMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGiversMVC.Controllers
{
    public class ProjectsController : Controller
    {
        private static List<ReliefProject> _projects = new()
    {
        new ReliefProject
        {
            Id = 1,
            ProjectCode = "KZN-FLOOD-2026",
            Name = "KZN Flood Relief",
            Description = "Emergency relief for flood-affected communities in KwaZulu-Natal",
            Location = "Durban, KZN",
            DisasterType = "Flood",
            Severity = "Critical",
            StartDate = DateTime.Now.AddDays(-15),
            Budget = 2500000,
            AmountRaised = 1750000,
            VolunteerTarget = 200,
            VolunteerCount = 145,
            Status = "Active",
            ImageUrl = "/images/projects/flood-relief.jpg"      // ← Must exist
        },
        new ReliefProject
        {
            Id = 2,
            ProjectCode = "EC-DROUGHT-2026",
            Name = "Eastern Cape Drought Response",
            Description = "Water delivery and agricultural support for drought-affected farms",
            Location = "Eastern Cape",
            DisasterType = "Drought",
            Severity = "High",
            StartDate = DateTime.Now.AddDays(-45),
            Budget = 1800000,
            AmountRaised = 1200000,
            VolunteerTarget = 100,
            VolunteerCount = 78,
            Status = "Active",
            ImageUrl = "/images/projects/drought-relief.jpg"    // ← Must exist
        },
        new ReliefProject
        {
            Id = 3,
            ProjectCode = "GP-FIRE-2026",
            Name = "Gauteng Informal Settlement Fire",
            Description = "Shelter and food relief after fire destroyed informal homes",
            Location = "Johannesburg, Gauteng",
            DisasterType = "Fire",
            Severity = "High",
            StartDate = DateTime.Now.AddDays(-7),
            Budget = 800000,
            AmountRaised = 620000,
            VolunteerTarget = 80,
            VolunteerCount = 65,
            Status = "Active",
            ImageUrl = "/images/projects/fire-relief.jpg"       // ← Must exist
        }
    };

        public IActionResult Index()
        {
            return View(_projects);
        }

        public IActionResult Details(int id)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null) return NotFound();
            return View(project);
        }
    }

}