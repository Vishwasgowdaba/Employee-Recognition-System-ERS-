using Microsoft.AspNetCore.Mvc;
using Employee_Recognition_System.Services.Interfaces;
using Employee_Recognition_System.DTOs.Nomination;

namespace Employee_Recognition_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NominationController : ControllerBase
    {
        private readonly INominationService _service;

        public NominationController(INominationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNominationDTO dto)
        {
            var result = await _service.Create(dto);
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();
            return Ok(data);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateNominationStatusDTO dto)
        {
            var result = await _service.UpdateStatus(id, dto);
            return Ok(result);
        }
    }
}