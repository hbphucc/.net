
using FashionShopAPI.Data;
using FashionShopAPI.Models.DTOs;
using FashionShopAPI.Models.Entities;
using FashionShopAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FashionShopAPI.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly FashionShopDbContext _context;

        public ProductRepository(FashionShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllActiveAsync(string? search, string? categoryId, string? gender)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductSizes)
                .Where(p => p.Status == true)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.ProductName.Contains(search));

            if (!string.IsNullOrEmpty(categoryId))
                query = query.Where(p => p.CategoryId == categoryId);

            if (!string.IsNullOrEmpty(gender))
                query = query.Where(p => p.Category.Gender == gender);

            return await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(string productId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductSizes)
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.Status == true);
        }

        public async Task AddAsync(Product product, Dictionary<string, int> sizes)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Products.AddAsync(product);

                foreach (var size in sizes)
                {
                    await _context.ProductSizes.AddAsync(new ProductSize
                    {
                        ProductId = product.ProductId,
                        Size = size.Key,
                        Stock = size.Value
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task SoftDeleteAsync(string productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Status = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<PagedResult<Product>> GetAllActiveAsync(string? search, string? categoryId, string? gender, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductSizes)
                .Where(p => p.Status == true)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.ProductName.Contains(search));

            if (!string.IsNullOrEmpty(categoryId))
                query = query.Where(p => p.CategoryId == categoryId);

            if (!string.IsNullOrEmpty(gender))
                query = query.Where(p => p.Category.Gender == gender);

            int totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}