using Microsoft.AspNetCore.Mvc;
using ProjectPierre.Data;
using ProjectPierre.DTO.CartItemDTOs;
using ProjectPierre.Interfaces;
using ProjectPierre.Mappers;
using ProjectPierre.Services;

namespace ProjectPierre.Controllers
{
    [Route("api/cartitem")]
    [ApiController]
    public class CartItemController : Controller
    {
        private readonly DataContext _context;
        private readonly ICartItemRepository _cartItemRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;

        public CartItemController(DataContext context, ICartItemRepository cartItemRepo, ICartRepository cartRepo, IProductRepository productRepo)
        {
            _context = context;
            _cartItemRepo = cartItemRepo;
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        [HttpGet("{cartItemId:int}")]
        public async Task<IActionResult> GetCartItemById([FromRoute] int cartItemId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cartItem = await _cartItemRepo.GetByIdAsync(cartItemId);

            if (cartItem == null)
            {
                return NotFound($"There is no cart item associated with the specified ID of {cartItemId}");
            }

            return Ok(cartItem.ToCartItemDTO());
        }

        [HttpPut("{cartItemId:int}")]
        public async Task<IActionResult> UpdateCartItem([FromRoute] int cartItemId, [FromBody] UpdateCartItemDTO updateCartItemDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cartItem = await _cartItemRepo.UpdateAsync(cartItemId, updateCartItemDTO);

            if (cartItem == null)
            {
                return BadRequest($"There is no cart item associated with the specified ID of {cartItemId}.");
            }

            return Ok(cartItem.ToCartItemDTOFromUpdatedCartItem());
        }

        [HttpDelete("{cartItemId:int}")]
        public async Task<IActionResult> DeleteCartItem([FromRoute] int cartItemId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cartItem = await _cartItemRepo.DeleteAsync(cartItemId);

            if (cartItem == null)
            {
                return BadRequest($"There is no cart item associated with the specified ID of {cartItemId}.");
            }

            return NoContent();
        }
    }
}
