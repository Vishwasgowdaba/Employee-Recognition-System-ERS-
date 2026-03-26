using Employee_Recognition_System.DTOs.Employee;

namespace Employee_Recognition_System.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponseDTO>> GetAll();
        Task<EmployeeResponseDTO> GetById(int id);
        Task<EmployeeResponseDTO> Create(CreateEmployeeDTO dto);
    }
}