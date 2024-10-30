using ProjectPierre.DTO.AisleDTOs;
using ProjectPierre.DTO.CategoryDTOs;

namespace ProjectPierre.DTO.ProductDTOs
{
    public class ProductCartItemDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
