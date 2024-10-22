using ProjectPierre.DTO.AisleDTOs;
using ProjectPierre.Models;

namespace ProjectPierre.Interfaces
{
    public interface IAisleRepository
    {
        Task<List<Aisle>> AddAsync(List<AddAisleDTO> aisleDTOs);
        Task<List<Aisle>?> DeleteAsync(List<RemoveAisleDTO> aisleDTOs);
    }
}
