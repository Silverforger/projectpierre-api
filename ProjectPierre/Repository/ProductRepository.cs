using Microsoft.EntityFrameworkCore;
using ProjectPierre.Data;
using ProjectPierre.DTO.AisleDTOs;
using ProjectPierre.DTO.ProductDTOs;
using ProjectPierre.Helpers;
using ProjectPierre.Interfaces;
using ProjectPierre.Mappers;
using ProjectPierre.Models;
using ProjectPierre.Services;

namespace ProjectPierre.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync(QueryObject query)
        {
            var products = _context.Products.Include(p => p.Aisles).ThenInclude(a => a.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Filter))
            {
                products = products.Where(p => p.Label.Contains(query.Filter) || p.Description.Contains(query.Filter));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDescending ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price);
                }

                if (query.SortBy.Equals("Label", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDescending ? products.OrderByDescending(p => p.Label) : products.OrderBy(p => p.Label);
                }
            }

            if (query.CategoryId != null)
            {
                products = products.Where(p => p.Aisles.Where(a => a.CategoryId == query.CategoryId).Any());
            }

            var pageSkip = (query.PageNumber -1) * query.PageSize;

            return await products.Skip(pageSkip).Take(query.PageSize).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Aisles).ThenInclude(a => a.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(CreateProductDTO createProductDTO)
        {
            var productModel = createProductDTO.ToProductFromCreateProductDTO();

            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();
            

            return productModel;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) {
                return null;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> UpdateAsync(int id, UpdateProductDTO updateProductDTO)
        {
            var product = await _context.Products.Include(p => p.Aisles).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) {
                return null;
            }

            product.Label = updateProductDTO.Label;
            product.Description = updateProductDTO.Description;
            product.Price = updateProductDTO.Price;

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<bool> IsValidProductId(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            return product == null ? false : true;
        }
    }
}
