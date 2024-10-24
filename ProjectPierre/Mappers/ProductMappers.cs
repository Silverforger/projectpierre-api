using ProjectPierre.DTO.CategoryDTOs;
using ProjectPierre.DTO.ProductDTOs;
using ProjectPierre.Models;

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
                //Aisles = product.Aisles.Select(a => new AisleDTO
                //{
                //    ProductId = a.ProductId,
                //    CategoryId = a.CategoryId,
                //}).ToList()
                Categories = product.Aisles.Select(a => new CategoriesListItemDTO
                {
                    Id = a.CategoryId,
                    Name = a.Category.Name
                }).ToList()
            };
        }

        public static ProductFetchDTO ToProductFetchDTO(this Product product)
        {
            return new ProductFetchDTO
            {
                Id = product.Id,
                Label = product.Label,
                Description = product.Description,
                Price = product.Price,
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
