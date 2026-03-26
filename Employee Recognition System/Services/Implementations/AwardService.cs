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

        public async Task<List<AwardCategoryResponseDTO>> GetAll()
        {
            return await _context.AwardCategories
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .Select(a => new AwardCategoryResponseDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Points = a.Points
                })
                .ToListAsync();
        }

        public async Task<AwardCategoryResponseDTO> GetById(int id)
        {
            var award = await _context.AwardCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (award == null)
                throw new KeyNotFoundException();

            return new AwardCategoryResponseDTO
            {
                Id = award.Id,
                Name = award.Name,
                Points = award.Points
            };
        }

        public async Task<AwardCategoryResponseDTO> Create(CreateAwardCategoryDTO dto)
        {
            var exists = await _context.AwardCategories
                .AnyAsync(a => a.Name.ToLower() == dto.Name.ToLower());

            if (exists)
                throw new InvalidOperationException("Award category already exists");

            var award = new AwardCategory
            {
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