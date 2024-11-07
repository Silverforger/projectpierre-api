using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectPierre.Data;
using ProjectPierre.DTO.ProductDTOs;
using ProjectPierre.Helpers;
using ProjectPierre.Interfaces;
using ProjectPierre.Mappers;

namespace ProjectPierre.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IProductRepository _productRepo;
        private readonly IAisleRepository _aisleRepo;
        private readonly ICategoryRepository _categoryRepo;

        public ProductController(DataContext context, IProductRepository productRepo, IAisleRepository aisleRepo, ICategoryRepository categoryRepo)
        {
            _context = context;
            _productRepo = productRepo;
            _aisleRepo = aisleRepo;
            _categoryRepo = categoryRepo;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] QueryObject query)
        {
            var products = await _productRepo.GetAllAsync(query);
            var productsDTO = products.Select(p => p.ToProductDTO());

            return Ok(productsDTO);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product == null) {
                return NotFound("There is no product associated with the specified ID.");
            }

            return Ok(product.ToProductDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hasNewAisles = false;
            if (createProductDTO.CategoryIds.Count > 0)
            {
                hasNewAisles = true;

                foreach (var catId in createProductDTO.CategoryIds) { 
                    if (!await _categoryRepo.IsValidCategoryId(catId))
                    {
                        return BadRequest($"There is no category associated with the provided category ID of {catId}.");
                    }
                }
            }
            var productModel = await _productRepo.CreateAsync(createProductDTO);

            if (hasNewAisles)
            {
                var aisleDTOs = createProductDTO.ToAisleDTOsFromCreateProductDTO(productModel.Id);

                await _aisleRepo.AddAsync(aisleDTOs);
            }

            return CreatedAtAction(nameof(GetProductById), new { id = productModel.Id }, productModel.ToProductDTO());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDTO updateProductDTO)
        {
            var product = await _productRepo.UpdateAsync(id, updateProductDTO);

            if (product == null)
            {
                return NotFound("There is no product associated with the specified ID.");
            }

            return Ok(product.ToProductDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await _productRepo.DeleteAsync(id);

            if (product == null)
            {
                return NotFound("There is no product associated with the specified ID.");
            }

            return NoContent();
        }
    }
}
