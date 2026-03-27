using Employee_Recognition_System.DTOs.Auth;

namespace Employee_Recognition_System.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> Register(RegisterDTO dto);
        Task<AuthResponseDTO> Login(LoginDTO dto);
    }
}