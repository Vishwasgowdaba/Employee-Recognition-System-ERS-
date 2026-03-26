using System.ComponentModel.DataAnnotations;

namespace Employee_Recognition_System.DTOs.Appreciation
{
    public class CreateAppreciationDTO
    {
        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [Required]
        [StringLength(300)]
        public string Message { get; set; }
    }
}