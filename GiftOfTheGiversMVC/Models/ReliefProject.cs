namespace GiftOfTheGiversMVC.Models
{
    public class ReliefProject
    {
        public int Id { get; set; }
        public string ProjectCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string DisasterType { get; set; } = string.Empty;
        public string Severity { get; set; } = "Medium";
        public DateTime StartDate { get; set; }
        public decimal Budget { get; set; }
        public decimal AmountRaised { get; set; }
        public int VolunteerTarget { get; set; }
        public int VolunteerCount { get; set; }
        public string Status { get; set; } = "Active";
        public string ImageUrl { get; set; } = string.Empty;   // ← Added this
    }
}