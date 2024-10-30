using ProjectPierre.DTO.CartItemDTOs;
using ProjectPierre.DTO.CategoryDTOs;
using ProjectPierre.Models;

namespace ProjectPierre.Mappers
{
    public static class CartItemMapper
    {
        public static CartItemDTO ToCartItemDTO(this CartItem cartItem)
        {
            return new CartItemDTO
            {
                Id = cartItem.Id,
                Count = cartItem.Count,
                ProductId = cartItem.ProductId,
                Product = cartItem.Product.ToProductCartItemDTO(),
                CartId = cartItem.CartId,
            };
        }

        public static CartItem ToCartItemFromAddCartItemDTO(this AddCartItemDTO cartItemDTO, int cartId)
        {
            return new CartItem
            {
                Count = cartItemDTO.Count,
                ProductId = cartItemDTO.ProductId,
                CartId = cartId,
            };
        }

        public static UpdatedCartItemDTO ToCartItemDTOFromUpdatedCartItem(this CartItem cartItem)
        {
            return new UpdatedCartItemDTO
            {
                Id = cartItem.Id,
                Count = cartItem.Count,
                ProductId = cartItem.ProductId,
                CartId = cartItem.CartId,
            };
        }
    }
}
