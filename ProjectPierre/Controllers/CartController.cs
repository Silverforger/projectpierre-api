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
    [Route("api/cart")]
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

        [HttpGet("/cartUserId/{cartUserId}")]
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

        [HttpGet("/cartId/{cartId:int}")]
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

        [HttpPost("/addToCart/{cartId:int}")]
        //[Authorize]
        public async Task<IActionResult> AddToCart([FromRoute] int cartId, List<AddCartItemDTO> addCartItemDTOs)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _cartRepo.IsValidCartId(cartId))
            {
                return BadRequest($"There is no cart associated with the specified cart Id of {cartId}.");
            }

            foreach (var product in addCartItemDTOs)
            {
                if (!await _productRepo.IsValidProductId(product.ProductId))
                {
                    return BadRequest($"There is no product associated with the specified ID of {product.ProductId}.");
                }
            }

            var cartItems = await _cartItemRepo.AddAsync(cartId, addCartItemDTOs);
            var cart = await _cartRepo.GetByCartIdAsync(cartId);

            return Ok(cart.ToCartDTO());
        }
    }
}
