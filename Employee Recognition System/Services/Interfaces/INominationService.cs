using Employee_Recognition_System.DTOs.Nomination;

namespace Employee_Recognition_System.Services.Interfaces
{
    public interface INominationService
    {
        Task<NominationResponseDTO> Create(CreateNominationDTO dto);
        Task<List<NominationResponseDTO>> GetAll();
        Task<NominationResponseDTO> UpdateStatus(int id, UpdateNominationStatusDTO dto);
    }
}