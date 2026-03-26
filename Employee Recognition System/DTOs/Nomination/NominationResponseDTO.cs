namespace Employee_Recognition_System.DTOs.Nomination
{
    public class NominationResponseDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public int AwardCategoryId { get; set; }
        public string AwardName { get; set; }

        public int NominatedById { get; set; }
        public string NominatedByName { get; set; }

        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}