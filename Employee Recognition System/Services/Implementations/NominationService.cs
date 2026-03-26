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

        // ✅ Create Nomination
        public async Task<NominationResponseDTO> Create(CreateNominationDTO dto)
        {
            if (dto.EmployeeId == dto.NominatedById)
                throw new Exception("You cannot nominate yourself");

            var employee = await _context.Employees.FindAsync(dto.EmployeeId)
                ?? throw new KeyNotFoundException("Employee not found");

            var award = await _context.AwardCategories.FindAsync(dto.AwardCategoryId)
                ?? throw new KeyNotFoundException("Award category not found");

            var nominator = await _context.Employees.FindAsync(dto.NominatedById)
                ?? throw new KeyNotFoundException("Nominator not found");

            // Prevent duplicate nominations
            var exists = await _context.Nominations.AnyAsync(n =>
                n.EmployeeId == dto.EmployeeId &&
                n.AwardCategoryId == dto.AwardCategoryId &&
                n.Status == NominationStatus.Pending);

            if (exists)
                throw new Exception("Already nominated for this award");

            var nomination = new Nomination
            {
                EmployeeId = dto.EmployeeId,
                AwardCategoryId = dto.AwardCategoryId,
                NominatedById = dto.NominatedById
            };

            await _context.Nominations.AddAsync(nomination);
            await _context.SaveChangesAsync();

            // 📧 Send Email
            await _emailService.SendEmailAsync(
                employee.Email,
                "You've been nominated!",
                $"You have been nominated for {award.Name}"
            );

            return MapToDTO(nomination, employee, award, nominator);
        }

        // ✅ Get All
        public async Task<List<NominationResponseDTO>> GetAll()
        {
            return await _context.Nominations
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

        // ✅ Update Status
        public async Task<NominationResponseDTO> UpdateStatus(int id, UpdateNominationStatusDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var nomination = await _context.Nominations
                .Include(n => n.Employee)
                .Include(n => n.AwardCategory)
                .FirstOrDefaultAsync(n => n.Id == id)
                ?? throw new Exception("Nomination not found");

            if (nomination.Status != NominationStatus.Pending)
                throw new Exception("Already processed");

            var newStatus = Enum.Parse<NominationStatus>(dto.Status, true);
            nomination.Status = newStatus;

            if (newStatus == NominationStatus.Approved)
            {
                nomination.Employee.Points += nomination.AwardCategory.Points;
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // 📧 Email
            await _emailService.SendEmailAsync(
                nomination.Employee.Email,
                "Nomination Update",
                $"Your nomination has been {newStatus}"
            );

            return new NominationResponseDTO
            {
                Id = nomination.Id,
                EmployeeId = nomination.EmployeeId,
                EmployeeName = nomination.Employee.Name,
                AwardCategoryId = nomination.AwardCategoryId,
                AwardName = nomination.AwardCategory.Name,
                NominatedById = nomination.NominatedById,
                Status = nomination.Status.ToString(),
                CreatedAt = nomination.CreatedAt
            };
        }

        // 🔁 Mapper
        private NominationResponseDTO MapToDTO(
            Nomination nomination,
            Employee employee,
            AwardCategory award,
            Employee nominator)
        {
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
    }
}