using ProjectPierre.DTO.AisleDTOs;
using ProjectPierre.DTO.CategoryDTOs;
using ProjectPierre.Models;

namespace ProjectPierre.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDTO ToCategoryDTO(this Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Aisles = category.Aisles.Select(a => new AisleDTO
                {
                    ProductId = a.ProductId,
                    CategoryId = a.CategoryId,
                }).ToList()
            };
        }

        public static Category ToCategoryFromAddCategoryDTO(this AddCategoryDTO addCategoryDTO) {
            return new Category
            {
                Name = addCategoryDTO.Name,
                Aisles = new List<Aisle>()
            };
        }
    }
}
