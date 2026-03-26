using Employee_Recognition_System.Data;
using Employee_Recognition_System.DTOs.Dashboard;
using Employee_Recognition_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Recognition_System.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        // 🏆 Leaderboard
        public async Task<List<LeaderboardDTO>> GetLeaderboard()
        {
            return await _context.Employees
                .AsNoTracking()
                .OrderByDescending(e => e.Points)
                .Take(10) // top 10
                .Select(e => new LeaderboardDTO
                {
                    EmployeeId = e.Id,
                    Name = e.Name,
                    Points = e.Points
                })
                .ToListAsync();
        }

        // 📢 Recent Activity
        public async Task<List<RecentActivityDTO>> GetRecentActivity()
        {
            var appreciations = await _context.Appreciations
                .Include(a => a.Sender)
                .Include(a => a.Receiver)
                .Select(a => new RecentActivityDTO
                {
                    Type = "Appreciation",
                    Message = a.Sender.Name + " appreciated " + a.Receiver.Name,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            var nominations = await _context.Nominations
                .Include(n => n.Employee)
                .Include(n => n.AwardCategory)
                .Select(n => new RecentActivityDTO
                {
                    Type = "Nomination",
                    Message = n.Employee.Name + " nominated for " + n.AwardCategory.Name,
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync();

            return appreciations
                .Concat(nominations)
                .OrderByDescending(x => x.CreatedAt)
                .Take(10)
                .ToList();
        }
    }
}