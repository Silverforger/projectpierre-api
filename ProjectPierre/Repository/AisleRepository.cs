using ProjectPierre.Data;
using ProjectPierre.DTO.AisleDTOs;
using ProjectPierre.Interfaces;
using ProjectPierre.Mappers;
using ProjectPierre.Models;

namespace ProjectPierre.Repository
{
    public class AisleRepository : IAisleRepository
    {
        private readonly DataContext _context;

        public AisleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Aisle>> AddAsync(List<AddAisleDTO> aisleDTOs)
        {
            var aisles = aisleDTOs.Select(a => a.ToAisleFromAddAisleDTO()).ToList();

            await _context.Aisles.AddRangeAsync(aisles);
            await _context.SaveChangesAsync();

            return aisles;
        }

        public async Task<List<Aisle>?> DeleteAsync(List<RemoveAisleDTO> aisleDTOs)
        {
            var aisles = aisleDTOs.Select(a => a.ToAisleFromRemoveAisleDTO()).ToList();

            _context.Aisles.RemoveRange(aisles);
            await _context.SaveChangesAsync();

            return aisles;
        }
    }
}
