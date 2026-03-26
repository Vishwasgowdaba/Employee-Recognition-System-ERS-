using Employee_Recognition_System.Data;
using Employee_Recognition_System.DTOs.Nomination;
using Employee_Recognition_System.Models;
using Employee_Recognition_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Recognition_System.Services.Implementations
{
    public class NominationService : INominationService
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public NominationService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // ✅ CREATE
        public async Task<NominationResponseDTO> Create(CreateNominationDTO dto)
        {
            if (dto.EmployeeId == dto.NominatedById)
                throw new InvalidOperationException("You cannot nominate yourself");

            var employee = await _context.Employees.FindAsync(dto.EmployeeId)
                ?? throw new KeyNotFoundException("Employee not found");

            var award = await _context.AwardCategories.FindAsync(dto.AwardCategoryId)
                ?? throw new KeyNotFoundException("Award not found");

            var nominator = await _context.Employees.FindAsync(dto.NominatedById)
                ?? throw new KeyNotFoundException("Nominator not found");

            var exists = await _context.Nominations.AnyAsync(n =>
                n.EmployeeId == dto.EmployeeId &&
                n.AwardCategoryId == dto.AwardCategoryId &&
                n.Status == NominationStatus.Pending);

            if (exists)
                throw new InvalidOperationException("Already nominated");

            var nomination = new Nomination
            {
                EmployeeId = dto.EmployeeId,
                AwardCategoryId = dto.AwardCategoryId,
                NominatedById = dto.NominatedById
            };

            await _context.Nominations.AddAsync(nomination);
            await _context.SaveChangesAsync();

            try
            {
                await _emailService.SendEmailAsync(
                    employee.Email,
                    "You've been nominated!",
                    $"You have been nominated for {award.Name}"
                );
            }
            catch { }

            return new NominationResponseDTO
            {
                Id = nomination.Id,
                EmployeeId = employee.Id,
                EmployeeName = employee.Name,
                AwardCategoryId = award.Id,
                AwardName = award.Name,
                NominatedById = nominator.Id,
                NominatedByName = nominator.Name,
                Status = nomination.Status.ToString(),
                CreatedAt = nomination.CreatedAt
            };
        }

        // ✅ GET ALL
        public async Task<List<NominationResponseDTO>> GetAll()
        {
            return await _context.Nominations
                .AsNoTracking()
                .Include(n => n.Employee)
                .Include(n => n.AwardCategory)
                .Include(n => n.NominatedBy)
                .Select(n => new NominationResponseDTO
                {
                    Id = n.Id,
                    EmployeeId = n.EmployeeId,
                    EmployeeName = n.Employee.Name,
                    AwardCategoryId = n.AwardCategoryId,
                    AwardName = n.AwardCategory.Name,
                    NominatedById = n.NominatedById,
                    NominatedByName = n.NominatedBy.Name,
                    Status = n.Status.ToString(),
                    CreatedAt = n.CreatedAt
                })
                .ToListAsync();
        }

        // ✅ UPDATE STATUS
        public async Task<NominationResponseDTO> UpdateStatus(int id, UpdateNominationStatusDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var nomination = await _context.Nominations
                .Include(n => n.AwardCategory)
                .Include(n => n.Employee)
                .Include(n => n.NominatedBy)
                .FirstOrDefaultAsync(n => n.Id == id)
                ?? throw new KeyNotFoundException("Nomination not found");

            if (!Enum.TryParse<NominationStatus>(dto.Status, true, out var newStatus))
                throw new InvalidOperationException("Invalid status");

            if (nomination.Status != NominationStatus.Pending)
                throw new InvalidOperationException("Already processed");

            nomination.Status = newStatus;

            if (newStatus == NominationStatus.Approved)
            {
                nomination.Employee.Points += nomination.AwardCategory.Points;
            }

            await _context.SaveChangesAsync();

            try
            {
                await _emailService.SendEmailAsync(
                    nomination.Employee.Email,
                    "Nomination Update",
                    $"Your nomination has been {newStatus}"
                );
            }
            catch { }

            await transaction.CommitAsync();

            return new NominationResponseDTO
            {
                Id = nomination.Id,
                EmployeeId = nomination.EmployeeId,
                EmployeeName = nomination.Employee.Name,
                AwardCategoryId = nomination.AwardCategoryId,
                AwardName = nomination.AwardCategory.Name,
                NominatedById = nomination.NominatedById,
                NominatedByName = nomination.NominatedBy.Name,
                Status = nomination.Status.ToString(),
                CreatedAt = nomination.CreatedAt
            };
        }
    }
}