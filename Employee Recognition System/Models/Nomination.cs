using Employee_Recognition_System.Models;

public class Nomination
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public int NominatedById { get; set; }
    public Employee NominatedBy { get; set; }

    public int AwardCategoryId { get; set; }
    public AwardCategory AwardCategory { get; set; }

    public NominationStatus Status { get; set; } = NominationStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}