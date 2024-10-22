using Microsoft.EntityFrameworkCore;
using ProjectPierre.Data;
using ProjectPierre.DTO.CategoryDTOs;
using ProjectPierre.Interfaces;
using ProjectPierre.Mappers;
using ProjectPierre.Models;

namespace ProjectPierre.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.Aisles).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.Include(c => c.Aisles).FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            return category;
        }

        public async Task<Category> CreateAsync(AddCategoryDTO addCategoryDTO)
        {
            var categoryModel = addCategoryDTO.ToCategoryFromAddCategoryDTO();

            await _context.Categories.AddAsync(categoryModel);
            await _context.SaveChangesAsync();

            return categoryModel;
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return null;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> UpdateAsync(int id, UpdateCategoryDTO updateCategoryDTO)
        {
            var category = await _context.Categories.Include(c => c.Aisles).FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return null;
            }

            category.Name = updateCategoryDTO.Name;
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<bool> IsValidCategoryId(int id)
        {
            var product = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);

            return product == null ? false : true;
        }
    }
}
