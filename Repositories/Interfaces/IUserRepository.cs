using FashionShopAPI.Models.Entities;

namespace FashionShopAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> AuthenticateAsync(string email, string password);
        Task<bool> RegisterAsync(User user);
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetByIdAsync(string userId);
        Task<bool> UpdateProfileAsync(User user);
        Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    }
}