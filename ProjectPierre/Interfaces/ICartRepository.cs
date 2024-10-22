using ProjectPierre.DTO.CartItemDTOs;
using ProjectPierre.Models;

namespace ProjectPierre.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetByCartIdAsync(int cartId);
        Task<Cart?> GetByUserIdAsync(string cartUserId);
        Task<Cart?> CreateCartAsync(string cartUserId);
        Task<bool> IsValidCartId(int cartId);
    }
}
