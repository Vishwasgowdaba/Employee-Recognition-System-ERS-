using Microsoft.AspNetCore.Mvc;
using Employee_Recognition_System.Services.Interfaces;
using Employee_Recognition_System.DTOs.Award;

namespace Employee_Recognition_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AwardController : ControllerBase
    {
        private readonly IAwardService _service;

        public AwardController(IAwardService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all award categories
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
                    message = "Awards fetched successfully",
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
        /// Get award by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid award ID");

                var award = await _service.GetById(id);

                if (award == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Award not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = award
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
        /// Create new award category
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAwardCategoryDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Invalid request data");

                var result = await _service.Create(dto);

                return Ok(new
                {
                    success = true,
                    message = "Award created successfully",
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