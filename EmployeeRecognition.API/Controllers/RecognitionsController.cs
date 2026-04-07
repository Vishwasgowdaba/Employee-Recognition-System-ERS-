// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using EmployeeRecognition.API.Data;
// using EmployeeRecognition.API.DTOs;
// using EmployeeRecognition.API.Models;
// using System.Security.Claims;

// namespace EmployeeRecognition.API.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// [Authorize]
// public class RecognitionsController : ControllerBase
// {
//     private readonly AppDbContext _db;

//     public RecognitionsController(AppDbContext db) => _db = db;

//     [HttpGet]
//     public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] string? type)
//     {
//         var query = _db.Recognitions
//             .Include(r => r.FromEmployee)
//             .Include(r => r.ToEmployee)
//             .Include(r => r.Category)
//             .AsQueryable();

//         if (!string.IsNullOrEmpty(status))
//             query = query.Where(r => r.Status == status);
//         if (!string.IsNullOrEmpty(type))
//             query = query.Where(r => r.Type == type);

//         var results = await query.OrderByDescending(r => r.CreatedAt).ToListAsync();
//         return Ok(results);
//     }

//     [HttpGet("my")]
//     public async Task<IActionResult> GetMy()
//     {
//         var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

//         var results = await _db.Recognitions
//             .Include(r => r.FromEmployee)
//             .Include(r => r.ToEmployee)
//             .Include(r => r.Category)
//             .Where(r => r.FromEmployeeId == userId || r.ToEmployeeId == userId)
//             .OrderByDescending(r => r.CreatedAt)
//             .ToListAsync();

//         return Ok(results);
//     }

//     [HttpPost]
//     public async Task<IActionResult> Create([FromBody] CreateRecognitionRequest req)
//     {
//         var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
//         var userRole = User.FindFirstValue("userRole")!;

//         int points = 50; // default appreciation points
//         string status = "approved";

//         if (req.CategoryId.HasValue)
//         {
//             var category = await _db.AwardCategories.FindAsync(req.CategoryId.Value);
//             if (category == null) return BadRequest(new { message = "Invalid category" });

//             if (category.ManagerOnly && userRole == "employee")
//                 return Forbid();

//             points = category.Points;
//         }

//         if (req.Type == "nomination")
//             status = "pending";

//         var recognition = new Recognition
//         {
//             FromEmployeeId = userId,
//             ToEmployeeId = req.ToEmployeeId,
//             Message = req.Message,
//             CategoryId = req.CategoryId,
//             Points = points,
//             Type = req.Type,
//             Status = status,
//             CreatedAt = DateTime.UtcNow
//         };

//         _db.Recognitions.Add(recognition);

//         // Auto-assign points for approved appreciations
//         if (status == "approved")
//         {
//             var toEmployee = await _db.Employees.FindAsync(req.ToEmployeeId);
//             if (toEmployee != null) toEmployee.TotalPoints += points;
//         }

//         await _db.SaveChangesAsync();

//         // Reload with includes
//         var result = await _db.Recognitions
//             .Include(r => r.FromEmployee)
//             .Include(r => r.ToEmployee)
//             .Include(r => r.Category)
//             .FirstAsync(r => r.Id == recognition.Id);

//         return Ok(result);
//     }

//     [HttpPut("{id}/approve")]
//     public async Task<IActionResult> Approve(int id)
//     {
//         var userRole = User.FindFirstValue("userRole")!;
//         if (userRole != "manager" && userRole != "admin")
//             return Forbid();

//         var recognition = await _db.Recognitions.Include(r => r.ToEmployee).FirstOrDefaultAsync(r => r.Id == id);
//         if (recognition == null) return NotFound();

//         recognition.Status = "approved";
//         recognition.ToEmployee.TotalPoints += recognition.Points;
//         await _db.SaveChangesAsync();

//         return NoContent();
//     }

//     [HttpPut("{id}/reject")]
//     public async Task<IActionResult> Reject(int id)
//     {
//         var userRole = User.FindFirstValue("userRole")!;
//         if (userRole != "manager" && userRole != "admin")
//             return Forbid();

//         var recognition = await _db.Recognitions.FirstOrDefaultAsync(r => r.Id == id);
//         if (recognition == null) return NotFound();

//         recognition.Status = "rejected";
//         await _db.SaveChangesAsync();

//         return NoContent();
//     }
// }
