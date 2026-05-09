using FashionShopAPI.Models.DTOs;
using FashionShopAPI.Models.Entities;

namespace FashionShopAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllActiveAsync(string? search, string? categoryId, string? gender);
        Task<Product?> GetByIdAsync(string productId);
        Task AddAsync(Product product, Dictionary<string, int> sizes);
        Task SoftDeleteAsync(string productId);
        Task<PagedResult<Product>> GetAllActiveAsync(string? search, string? categoryId, string? gender, int pageNumber = 1, int pageSize = 10);
    }
}