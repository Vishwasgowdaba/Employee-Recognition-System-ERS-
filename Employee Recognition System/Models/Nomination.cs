using System.ComponentModel.DataAnnotations;

namespace Employee_Recognition_System.Models
{
    public class Nomination
    {
        public int Id { get; set; }

        // 🔗 Employee being nominated
        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        // 🏆 Award category
        [Required]
        public int AwardCategoryId { get; set; }
        public AwardCategory AwardCategory { get; set; }

        // 👤 Who nominated
        [Required]
        public int NominatedById { get; set; }
        public Employee NominatedBy { get; set; }

        // 📌 Status
        public NominationStatus Status { get; set; } = NominationStatus.Pending;

        // 🕒 Timestamp
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}