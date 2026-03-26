using System.ComponentModel.DataAnnotations;

namespace Employee_Recognition_System.DTOs.Nomination
{
    public class UpdateNominationStatusDTO
    {
        [Required]
        public string Status { get; set; } // "Approved" / "Rejected"
    }
}