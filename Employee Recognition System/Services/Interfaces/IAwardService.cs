
    using Employee_Recognition_System.DTOs.Award;
    using global::Employee_Recognition_System.DTOs.Award;

    namespace Employee_Recognition_System.Services.Interfaces
    {
        public interface IAwardService
        {
            Task<List<AwardCategoryResponseDTO>> GetAll();
            Task<AwardCategoryResponseDTO> GetById(int id);
            Task<AwardCategoryResponseDTO> Create(CreateAwardCategoryDTO dto);
        }
    }

