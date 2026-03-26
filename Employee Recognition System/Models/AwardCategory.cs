using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Employee_Recognition_System.Models
{
    public class AwardCategory
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
    }
}