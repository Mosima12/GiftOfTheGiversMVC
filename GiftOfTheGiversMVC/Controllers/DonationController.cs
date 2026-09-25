using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGiversMVC.Controllers
{
    public class DonationController : Controller
    {
        // GET: /Donation
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessDonation(decimal amount, string currency,
     bool isRecurring, bool isAnonymous)
        {
            string certificateNumber = $"GOTG-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            string formattedAmount = currency switch
            {
                "ZAR" => $"R{amount:N2}",
                "USD" => $"${amount:N2}",
                "EUR" => $"€{amount:N2}",
                _ => $"{amount:N2} {currency}"
            };

            // Save donation record (in-memory)
            DonorController.AddDonation(new DonationRecord
            {
                Id = DonorController._donations.Count + 1,
                DonorEmail = User.Identity?.Name ?? "Anonymous",
                Amount = amount,
                Currency = currency,
                CertificateNumber = certificateNumber,
                Date = DateTime.Now,
                IsRecurring = isRecurring
            });

            ViewBag.Amount = formattedAmount;
            ViewBag.RawAmount = amount;
            ViewBag.Currency = currency;
            ViewBag.CertificateNumber = certificateNumber;
            ViewBag.IsRecurring = isRecurring;
            ViewBag.IsAnonymous = isAnonymous;
            ViewBag.DonationDate = DateTime.Now.ToString("dd MMMM yyyy HH:mm");

            return View("DonationConfirmation");
        }

        // GET: /Donation/TaxCertificate
        public IActionResult TaxCertificate(string certificateNumber)
        {
            if (string.IsNullOrEmpty(certificateNumber))
            {
                return RedirectToAction("Index");
            }

            ViewBag.CertificateNumber = certificateNumber;
            ViewBag.IssueDate = DateTime.Now.ToString("dd MMMM yyyy");
            ViewBag.TaxYear = DateTime.Now.Year;
            return View();
        }
    }
}