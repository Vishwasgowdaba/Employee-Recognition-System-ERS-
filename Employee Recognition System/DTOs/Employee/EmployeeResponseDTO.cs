namespace Employee_Recognition_System.DTOs.Employee
{
    public class EmployeeResponseDTO
    {
        public int employeeId { get; set; }
        public string Name { get; set; } = "";  
        public string Email { get; set; } = "";
        public int Points { get; set; }
    }
}
