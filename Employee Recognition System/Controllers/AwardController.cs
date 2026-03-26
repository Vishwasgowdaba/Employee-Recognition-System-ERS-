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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();

            return Ok(new
            {
                success = true,
                data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid award ID");

            try
            {
                var award = await _service.GetById(id);

                return Ok(new
                {
                    success = true,
                    data = award
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Award not found"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAwardCategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.Create(dto);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, new
                {
                    success = true,
                    data = result
                });
            }
            catch (InvalidOperationException ex)
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