using FashionShopAPI.Models.Entities;

namespace FashionShopAPI.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllActiveAsync();
        Task<Category?> GetByIdAsync(string id);
        Task<bool> AddAsync(Category category);
        Task<bool> UpdateAsync(Category category);
        Task<bool> SoftDeleteAsync(string id);
    }
}