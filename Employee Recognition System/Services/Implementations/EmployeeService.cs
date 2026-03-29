using Employee_Recognition_System.Data;
using Employee_Recognition_System.DTOs.Employee;
using Employee_Recognition_System.Models;
using Employee_Recognition_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Recognition_System.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeResponseDTO>> GetAll()
        {
            return await _context.Employees
                .Select(e => new EmployeeResponseDTO
                {
                    employeeId = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Points = e.Points
                })
                .ToListAsync();
        }

        public async Task<EmployeeResponseDTO> GetById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return null;

            return new EmployeeResponseDTO
            {
                employeeId = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Points = employee.Points
            };
        }

        public async Task<EmployeeResponseDTO> Create(CreateEmployeeDTO dto)
        {
           var employee = new Employee
{
    Name = dto.Name,
    Email = dto.Email,
    Points = dto.Points,
    PasswordHash = dto.PasswordHash,
    Role = dto.Role
};

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return new EmployeeResponseDTO
            {
                employeeId = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Points = employee.Points
            };
        }
    }
}