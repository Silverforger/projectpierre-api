using ProjectPierre.DTO.ProductDTOs;
using ProjectPierre.Helpers;
using ProjectPierre.Models;

namespace ProjectPierre.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(QueryObject query);
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(CreateProductDTO createProductDTO);
        Task<Product?> UpdateAsync(int id, UpdateProductDTO updateProductDTO);
        Task<Product?> DeleteAsync(int id);
        Task<bool> IsValidProductId(int id);
    }
}
