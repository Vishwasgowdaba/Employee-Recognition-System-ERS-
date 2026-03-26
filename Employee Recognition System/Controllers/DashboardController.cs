using Microsoft.AspNetCore.Mvc;
using Employee_Recognition_System.Services.Interfaces;

namespace Employee_Recognition_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        // 🏆 Leaderboard
        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            var data = await _service.GetLeaderboard();

            return Ok(new
            {
                success = true,
                data
            });
        }

        // 📢 Recent Activity
        [HttpGet("recent-activity")]
        public async Task<IActionResult> GetRecentActivity()
        {
            var data = await _service.GetRecentActivity();

            return Ok(new
            {
                success = true,
                data
            });
        }
    }
}