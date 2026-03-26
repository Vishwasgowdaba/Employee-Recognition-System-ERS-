namespace Employee_Recognition_System.DTOs.Dashboard
{
    public class RecentActivityDTO
    {
        public string Type { get; set; } // Appreciation / Nomination
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
