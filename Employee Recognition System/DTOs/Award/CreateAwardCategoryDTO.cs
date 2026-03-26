using System.ComponentModel.DataAnnotations;

namespace Employee_Recognition_System.DTOs.Award
{
    public class CreateAwardCategoryDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int Points { get; set; }
    }
}