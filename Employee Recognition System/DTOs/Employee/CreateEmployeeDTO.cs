namespace Employee_Recognition_System.DTOs.Employee
{
    public class CreateEmployeeDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Points { get; set; }
    public string PasswordHash { get; set; }  // ✅ ADD THIS
    public string Role { get; set; }
}
}
