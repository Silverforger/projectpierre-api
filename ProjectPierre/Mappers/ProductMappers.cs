using ProjectPierre.DTO.AisleDTOs;
using ProjectPierre.DTO.ProductDTOs;
using ProjectPierre.Models;
using System.Runtime.CompilerServices;

namespace ProjectPierre.Mappers
{
    public static class ProductMappers
    {
        public static ProductDTO ToProductDTO(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Label = product.Label,
                Description = product.Description,
                Price = product.Price,
                Aisles = product.Aisles.Select(a => new AisleDTO
                {
                    ProductId = a.ProductId,
                    CategoryId = a.CategoryId,
                }).ToList()
            };
        }

        public static Product ToProductFromCreateProductDTO(this CreateProductDTO product)
        {
            return new Product
            {
                Label = product.Label,
                Description = product.Description,
                Price = product.Price
            };
        }
    }
}
