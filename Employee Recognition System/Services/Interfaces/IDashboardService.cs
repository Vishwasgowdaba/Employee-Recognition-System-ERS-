using Employee_Recognition_System.DTOs.Dashboard;

namespace Employee_Recognition_System.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<List<LeaderboardDTO>> GetLeaderboard();
        Task<List<RecentActivityDTO>> GetRecentActivity();
    }
}