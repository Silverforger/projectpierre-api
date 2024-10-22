using ProjectPierre.DTO.AisleDTOs;
using ProjectPierre.DTO.ProductDTOs;
using ProjectPierre.Models;

namespace ProjectPierre.Mappers
{
    public static class AisleMapper
    {
        public static Aisle ToAisleFromAddAisleDTO (this AddAisleDTO aisleDTO)
        {
            return new Aisle
            {
                ProductId = aisleDTO.ProductId,
                CategoryId = aisleDTO.CategoryId
            };
        }

        public static Aisle ToAisleFromRemoveAisleDTO(this RemoveAisleDTO aisleDTO)
        {
            return new Aisle
            {
                ProductId = aisleDTO.ProductId,
                CategoryId = aisleDTO.CategoryId
            };
        }

        public static List<AddAisleDTO> ToAisleDTOsFromCreateProductDTO(this CreateProductDTO createProductDTO, int productId)
        {
            var aisles = new List<AddAisleDTO>();
            foreach (var catId in createProductDTO.CategoryIds)
            {
                var aisle = new AddAisleDTO
                {
                    ProductId = productId,
                    CategoryId = catId
                };

                aisles.Add(aisle);
            }

            return aisles;
        }
    }
}
