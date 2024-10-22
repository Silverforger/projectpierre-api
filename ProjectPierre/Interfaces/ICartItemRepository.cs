using ProjectPierre.DTO.CartItemDTOs;
using ProjectPierre.Models;

namespace ProjectPierre.Interfaces
{
    public interface ICartItemRepository
    {
        Task<CartItem?> GetByIdAsync(int cartItemId);
        Task<List<CartItem>> AddAsync(int cartId, List<AddCartItemDTO> cartItems);
        Task<CartItem?> UpdateAsync(int cartItemId, UpdateCartItemDTO updateCartItemDTO);
        Task<CartItem?> DeleteAsync(int cartItemId);
    }
}
