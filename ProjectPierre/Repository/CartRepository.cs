using Microsoft.EntityFrameworkCore;
using ProjectPierre.Data;
using ProjectPierre.DTO.CartDTOs;
using ProjectPierre.Interfaces;
using ProjectPierre.Mappers;
using ProjectPierre.Models;

namespace ProjectPierre.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;

        public CartRepository(DataContext context)
        {
            _context = context; 
        }

        public async Task<Cart?> GetByCartIdAsync(int cartId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(c => c.Product).ThenInclude(p => p.Aisles).FirstOrDefaultAsync(u => u.Id == cartId);

            if (cart == null)
            {
                return null;
            }

            return cart;
        }

        public async Task<Cart?> GetByUserIdAsync(string cartUserId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(c => c.Product).FirstOrDefaultAsync(u => u.CartUserId == cartUserId);

            if (cart == null)
            {
                return null;
            }

            return cart;
        } 

        public async Task<Cart?> CreateCartAsync(string cartUserId)
        {
            var cart = new CreateCartDTO { CartUserId = cartUserId };
            var cartModel = cart.ToCartFromCreateCartDTO();

            await _context.Carts.AddAsync(cartModel);
            await _context.SaveChangesAsync();

            return cartModel;
        }

        public async Task<bool> IsValidCartId(int cartId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);

            return cart == null ? false : true; 
        }
    }
}
