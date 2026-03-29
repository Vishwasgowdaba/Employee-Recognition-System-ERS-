using Employee_Recognition_System.Data;
using Employee_Recognition_System.DTOs.Auth;
using Employee_Recognition_System.Models;
using Employee_Recognition_System.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Employee_Recognition_System.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // 🔐 REGISTER
        public async Task<AuthResponseDTO> Register(RegisterDTO dto)
        {
            var exists = await _context.Employees
                .AnyAsync(e => e.Email == dto.Email);

            if (exists)
                throw new InvalidOperationException("User already exists");

            var user = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role ?? "Employee",
                Points = 0
            };

            await _context.Employees.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = GenerateToken(user);

            return new AuthResponseDTO
            {
                Token = token,
                Role = user.Role,
                Name = user.Name,
                UserId = user.Id
            };
        }

        // 🔐 LOGIN
        public async Task<AuthResponseDTO> Login(LoginDTO dto)
        {
            var user = await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = GenerateToken(user);

            return new AuthResponseDTO
            {
                Token = token,
                Role = user.Role,
                Name = user.Name,
                UserId = user.Id
            };
        }

        // 🎟 JWT GENERATION
        private string GenerateToken(Employee user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
         );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}