using Employee_Recognition_System.Data;
using Employee_Recognition_System.DTOs.Appreciation;
using Employee_Recognition_System.Models;
using Employee_Recognition_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Recognition_System.Services.Implementations
{
    public class AppreciationService : IAppreciationService
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public AppreciationService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<AppreciationResponseDTO> Add(CreateAppreciationDTO dto)
        {
            if (dto.SenderId == dto.ReceiverId)
                throw new Exception("You cannot appreciate yourself.");

            using var transaction = await _context.Database.BeginTransactionAsync();

            var sender = await _context.Employees.FindAsync(dto.SenderId)
                ?? throw new Exception("Sender not found");

            var receiver = await _context.Employees.FindAsync(dto.ReceiverId)
                ?? throw new Exception("Receiver not found");

            // 🚫 Optional: prevent spam (same sender repeatedly)
            var recentAppreciation = await _context.Appreciations.AnyAsync(a =>
                a.SenderId == dto.SenderId &&
                a.ReceiverId == dto.ReceiverId &&
                a.CreatedAt > DateTime.UtcNow.AddMinutes(-5));

            if (recentAppreciation)
                throw new Exception("You already appreciated this employee recently.");

            var appreciation = new Appreciation
            {
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                Message = dto.Message
            };

            await _context.Appreciations.AddAsync(appreciation);

            // 🎯 Assign points
            receiver.Points += 10;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // 📧 Send Email
            await _emailService.SendEmailAsync(
                receiver.Email,
                "You've been appreciated 🎉",
                $"{sender.Name} appreciated you:\n\n\"{dto.Message}\""
            );

            return new AppreciationResponseDTO
            {
                Id = appreciation.Id,
                SenderId = sender.Id,
               
                ReceiverId = receiver.Id,
              
                Message = appreciation.Message,
                CreatedAt = appreciation.CreatedAt
            };
        }

        public async Task<List<AppreciationResponseDTO>> GetAll()
{
    return await _context.Appreciations
        .Include(a => a.Sender)     // ✅ fetch sender
        .Include(a => a.Receiver)   // ✅ fetch receiver
        .OrderByDescending(a => a.CreatedAt)
        .Select(a => new AppreciationResponseDTO
        {
            Id = a.Id,
            SenderId = a.SenderId,
            SenderName = a.Sender.Name,       // ✅ add this
            ReceiverId = a.ReceiverId,
            ReceiverName = a.Receiver.Name,   // ✅ add this
            Message = a.Message,
            CreatedAt = a.CreatedAt
        })
        .ToListAsync();
}
    }
}