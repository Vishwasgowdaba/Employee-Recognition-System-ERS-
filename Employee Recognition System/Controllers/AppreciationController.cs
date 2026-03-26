using Employee_Recognition_System.DTOs.Appreciation;
using Employee_Recognition_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Recognition_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppreciationController : ControllerBase
    {
        private readonly IAppreciationService _service;

        public AppreciationController(IAppreciationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Send(CreateAppreciationDTO dto)
        {
            var result = await _service.Add(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();
            return Ok(data);
        }
    }
}