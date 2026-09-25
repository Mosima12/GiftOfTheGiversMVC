using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGiversMVC.Controllers
{
    [Authorize]
    public class DonorController : Controller
    {
        // In-memory donation storage
        public static List<DonationRecord> _donations = new();

        public IActionResult Dashboard()
        {
            var userEmail = User.Identity?.Name ?? "Anonymous";
            var userDonations = _donations
                .Where(d => d.DonorEmail == userEmail)
                .OrderByDescending(d => d.Date)
                .ToList();

            ViewBag.TotalDonated = userDonations.Sum(d => d.Amount);
            ViewBag.DonationCount = userDonations.Count;

            return View(userDonations);
        }

        public static void AddDonation(DonationRecord donation)
        {
            _donations.Add(donation);
        }
    }

    public class DonationRecord
    {
        public int Id { get; set; }
        public string DonorEmail { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "ZAR";
        public string CertificateNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; }
    }
}