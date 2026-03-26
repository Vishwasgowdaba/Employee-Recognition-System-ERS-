using System.ComponentModel.DataAnnotations;

namespace Employee_Recognition_System.DTOs.Award
{
    public class AwardCategoryResponseDTO
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
    }
}
