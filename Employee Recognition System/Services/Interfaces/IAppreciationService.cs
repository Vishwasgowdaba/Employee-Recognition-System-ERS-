using Employee_Recognition_System.DTOs.Appreciation;

namespace Employee_Recognition_System.Services.Interfaces
{
    public interface IAppreciationService
    {
        Task<AppreciationResponseDTO> Add(CreateAppreciationDTO dto);
        Task<List<AppreciationResponseDTO>> GetAll();
    }
}