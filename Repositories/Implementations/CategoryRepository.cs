using Microsoft.EntityFrameworkCore;
using FashionShopAPI.Data;
using FashionShopAPI.Models.Entities;
using FashionShopAPI.Repositories.Interfaces;

namespace FashionShopAPI.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FashionShopDbContext _context;

        public CategoryRepository(FashionShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllActiveAsync()
        {
            return await _context.Categories
                .Where(c => c.Status == true)
                .OrderBy(c => c.Gender).ThenBy(c => c.CategoryName)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(string id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id && c.Status == true);
        }

        public async Task<bool> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftDeleteAsync(string id)
        {
            var category = await GetByIdAsync(id);
            if (category == null) return false;

            category.Status = false;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}