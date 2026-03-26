using Employee_Recognition_System.Data;
using Employee_Recognition_System.DTOs.Award;
using Employee_Recognition_System.Models;
using Employee_Recognition_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee_Recognition_System.Services.Implementations
{
    public class AwardService : IAwardService
    {
        private readonly AppDbContext _context;

        public AwardService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Get all award categories
        public async Task<List<AwardCategoryResponseDTO>> GetAll()
        {
            return await _context.AwardCategories
                .Select(a => new AwardCategoryResponseDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Points = a.Points
                })
                .ToListAsync();
        }

        // ✅ Get award by ID
        public async Task<AwardCategoryResponseDTO> GetById(int id)
        {
            var award = await _context.AwardCategories.FindAsync(id);

            if (award == null)
                return null;

            return new AwardCategoryResponseDTO
            {
                Id = award.Id,
                Name = award.Name,
                Points = award.Points
            };
        }

        // ✅ Create new award category
        public async Task<AwardCategoryResponseDTO> Create(CreateAwardCategoryDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Award name is required");

            if (dto.Points <= 0)
                throw new Exception("Points must be greater than 0");

            // ❗ Check duplicate
            var exists = await _context.AwardCategories
                .AnyAsync(a => a.Name == dto.Name);

            if (exists)
                throw new Exception("Award category already exists");

            var award = new AwardCategory
            {   Id=dto.id,
                Name = dto.Name,
                Points = dto.Points
            };

            await _context.AwardCategories.AddAsync(award);
            await _context.SaveChangesAsync();

            return new AwardCategoryResponseDTO
            {
                Id = award.Id,
                Name = award.Name,
                Points = award.Points
            };
        }
    }
}