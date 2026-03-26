using System.ComponentModel.DataAnnotations;

namespace Employee_Recognition_System.DTOs.Nomination
{
    public class CreateNominationDTO
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int AwardCategoryId { get; set; }

        [Required]
        public int NominatedById { get; set; }
    }
}