using ProjectPierre.DTO.AisleDTOs;
using ProjectPierre.DTO.CartDTOs;
using ProjectPierre.DTO.CartItemDTOs;
using ProjectPierre.DTO.CategoryDTOs;
using ProjectPierre.DTO.ProductDTOs;
using ProjectPierre.Models;

namespace ProjectPierre.Mappers
{
    public static class CartMapper
    {
        public static CartDTO ToCartDTO(this Cart cart) {
            return new CartDTO
            {
                Id = cart.Id,
                CartUserId = cart.CartUserId,
                CartItems = cart.CartItems.Select(c => new CartItemDTO
                {
                    Id = c.Id,
                    Count = c.Count,
                    ProductId = c.ProductId,
                    Product = new ProductCartItemDTO
                    {
                        Id = c.Product.Id,
                        Label = c.Product.Label,
                        Description = c.Product.Description,
                        Price = c.Product.Price,
                        //Categories = c.Product.Aisles.Select(a => new CategoriesListItemDTO
                        //{
                        //    Id = a.CategoryId,
                        //    Name = a.Category.Name
                        //}).ToList()
                    },
                    CartId = cart.Id
                }).ToList()
            };
        }

        public static Cart ToCartFromCreateCartDTO(this CreateCartDTO createCartDTO)
        {
            return new Cart
            {
                CartUserId = createCartDTO.CartUserId,
            };
        }
    }
}
