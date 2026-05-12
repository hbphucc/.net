using Microsoft.EntityFrameworkCore;
using FashionShopAPI.Data;
using FashionShopAPI.Models.Entities;
using FashionShopAPI.Repositories.Interfaces;

namespace FashionShopAPI.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly FashionShopDbContext _context;

        public UserRepository(FashionShopDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.Status == true);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> RegisterAsync(User user)
        {
            if (await EmailExistsAsync(user.Email)) return false;

            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<User?> GetByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> UpdateProfileAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId && u.Password == oldPassword);
            if (user == null) return false; 

            user.Password = newPassword;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> UpdateUserStatusAsync(string userId, bool status)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return false;

            user.Status = status;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}