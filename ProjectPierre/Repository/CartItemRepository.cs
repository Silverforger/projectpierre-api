using Microsoft.EntityFrameworkCore;
using ProjectPierre.Data;
using ProjectPierre.DTO.CartItemDTOs;
using ProjectPierre.Interfaces;
using ProjectPierre.Mappers;
using ProjectPierre.Models;

namespace ProjectPierre.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataContext _context;

        public CartItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<CartItem?> GetByIdAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.Include(c => c.Product).ThenInclude(p => p.Aisles).FirstOrDefaultAsync(c => c.Id == cartItemId);

            if (cartItem == null)
            {
                return null;
            }

            return cartItem;
        }

        public async Task<CartItem> AddAsync(int cartId, AddCartItemDTO addCartItemDTO)
        {
            var cartItemsModel = addCartItemDTO.ToCartItemFromAddCartItemDTO(cartId);

            await _context.CartItems.AddAsync(cartItemsModel);
            await _context.SaveChangesAsync();

            return cartItemsModel;
        }

        public async Task<CartItem?> UpdateAsync(int cartItemId, UpdateCartItemDTO updateCartItemDTO)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            if (cartItem == null) {
                return null; 
            }

            cartItem.Count = updateCartItemDTO.Count;
            await _context.SaveChangesAsync();

            return cartItem;
        }

        public async Task<CartItem?> DeleteAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            if (cartItem == null)
            {
                return null;
            }

            _context.Remove(cartItem);
            await _context.SaveChangesAsync();

            return cartItem;
        }
    }
}
