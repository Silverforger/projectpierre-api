using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectPierre.Data;
using ProjectPierre.DTO.CategoryDTOs;
using ProjectPierre.Interfaces;
using ProjectPierre.Mappers;

namespace ProjectPierre.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase

    {
        private readonly DataContext _context;
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(DataContext context, ICategoryRepository categoryRepo)
        {
            _context = context;
            _categoryRepo = categoryRepo; 
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categories = await _categoryRepo.GetAllAsync();
            var categoriesDTO = categories.Select(c => c.ToCategoriesListItemDTO());

            return Ok(categoriesDTO);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category.ToCategoryDTO());
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDTO addCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepo.CreateAsync(addCategoryDTO);

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category.ToCategoryDTO());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepo.UpdateAsync(id, updateCategoryDTO);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category.ToCategoryDTO());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepo.DeleteAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
