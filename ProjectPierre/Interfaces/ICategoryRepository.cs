using ProjectPierre.DTO.CategoryDTOs;
using ProjectPierre.Models;

namespace ProjectPierre.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(AddCategoryDTO addCategoryDTO);
        Task<Category?> UpdateAsync(int id, UpdateCategoryDTO updateCategoryDTO);
        Task<Category> DeleteAsync(int id);
        Task<bool> IsValidCategoryId(int id);
    }
}
