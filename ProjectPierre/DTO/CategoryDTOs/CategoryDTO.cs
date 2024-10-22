using ProjectPierre.DTO.AisleDTOs;

namespace ProjectPierre.DTO.CategoryDTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AisleDTO> Aisles { get; set; }
    }
}
