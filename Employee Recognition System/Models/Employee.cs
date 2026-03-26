using System.ComponentModel.DataAnnotations;

namespace Employee_Recognition_System.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)] 
        public string Email { get; set; }
        public int Points { get; set; }
    }
}