using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectPierre.Data;
using ProjectPierre.DTO.CartItemDTOs;
using ProjectPierre.DTO.ProductDTOs;
using ProjectPierre.Interfaces;
using ProjectPierre.Mappers;
using ProjectPierre.Models;

namespace ProjectPierre.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICartRepository _cartRepo;
        private readonly ICartItemRepository _cartItemRepo;
        private readonly IProductRepository _productRepo;

        public CartController(DataContext context, ICartItemRepository cartItemRepo, ICartRepository cartRepo, IProductRepository productRepo)
        {
            _context = context;
            _cartRepo = cartRepo;  
            _productRepo = productRepo;
            _cartItemRepo = cartItemRepo;
        }

        //[Authorize]
        [HttpGet("cartUserId/{cartUserId}")]
        public async Task<IActionResult> GetCartByUserId([FromRoute] string cartUserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cart = await _cartRepo.GetByUserIdAsync(cartUserId);

            if (cart == null)
            {
                return NotFound("There is no cart associated with the specified cartUserId.");
            }

            return Ok(cart.ToCartDTO());
        }

        //[Authorize]
        [HttpGet("cartId/{cartId:int}")]
        public async Task<IActionResult> GetCartByCartId([FromRoute] int cartId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cart = await _cartRepo.GetByCartIdAsync(cartId);

            if (cart == null)
            {
                return NotFound("There is no cart associated with the specified cartId.");
            }

            return Ok(cart.ToCartDTO());
        }

        [HttpPost("addToCart/{cartId:int}")]
        //[Authorize]
        public async Task<IActionResult> AddToCart([FromRoute] int cartId, AddCartItemDTO addCartItemDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _cartRepo.IsValidCartId(cartId))
            {
                return BadRequest($"There is no cart associated with the specified cart Id of {cartId}.");
            }

            if (!await _productRepo.IsValidProductId(addCartItemDTO.ProductId))
            {
                return BadRequest($"There is no product associated with the specified ID of {addCartItemDTO.ProductId}.");
            }

            var cart = await _cartRepo.GetByCartIdAsync(cartId);
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == addCartItemDTO.ProductId, null);
            if (cartItem == null)
            {
                await _cartItemRepo.AddAsync(cartId, addCartItemDTO);
            } else
            {
                await _cartItemRepo.UpdateAsync(cartItem.Id, new UpdateCartItemDTO { Count = addCartItemDTO.Count });
            }

            return Ok(cart.ToCartDTO());
        }
    }
}
