using ProjectPierre.DTO.CartItemDTOs;
using ProjectPierre.Models;

namespace ProjectPierre.Interfaces
{
    public interface ICartItemRepository
    {
        Task<CartItem?> GetByIdAsync(int cartItemId);
        Task<CartItem> AddAsync(int cartId, AddCartItemDTO addCartItemDTO);
        Task<CartItem?> UpdateAsync(int cartItemId, UpdateCartItemDTO updateCartItemDTO);
        Task<CartItem?> DeleteAsync(int cartItemId);
    }
}
