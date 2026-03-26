using System.ComponentModel.DataAnnotations;

namespace Employee_Recognition_System.Models
{
    public class Appreciation
    {
        public int Id { get; set; }

        [Required]
        public int SenderId { get; set; }
        

        [Required]
        public int ReceiverId { get; set; }
        

        [Required]
        [StringLength(300)]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}