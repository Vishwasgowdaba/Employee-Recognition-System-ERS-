using Microsoft.AspNetCore.Mvc;
using Employee_Recognition_System.Services.Interfaces;
using Employee_Recognition_System.DTOs.Employee;

namespace Employee_Recognition_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _service.GetAll();

                return Ok(new
                {
                    success = true,
                    message = "Employees fetched successfully",
                    data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Get employee by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid employee ID");

                var employee = await _service.GetById(id);

                if (employee == null)
                    return NotFound(new
                    {
                        success = false,
                        message = "Employee not found"
                    });

                return Ok(new
                {
                    success = true,
                    data = employee
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Create new employee
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Invalid data");

                var result = await _service.Create(dto);

                return Ok(new
                {
                    success = true,
                    message = "Employee created successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}