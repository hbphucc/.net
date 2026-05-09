using Microsoft.EntityFrameworkCore;
using FashionShopAPI.Data;
using FashionShopAPI.Models.DTOs;
using FashionShopAPI.Repositories.Interfaces;

namespace FashionShopAPI.Repositories.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly FashionShopDbContext _context;

        public AdminRepository(FashionShopDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardResponse> GetDashboardStatsAsync()
        {
            var today = DateTime.Today;
            var currentMonth = today.Month;
            var currentYear = today.Year;

            return new DashboardResponse
            {
                TotalProducts = await _context.Products.CountAsync(),
                TotalOrders = await _context.Orders.CountAsync(),
                TotalUsers = await _context.Users.CountAsync(),

                NewOrdersCount = await _context.Orders
                    .CountAsync(o => o.Status == "pending"),

                TodayRevenue = await _context.Orders
                    .Where(o => o.Status == "delivered" && o.CreatedAt >= today)
                    .SumAsync(o => o.TotalAmount),

                MonthRevenue = await _context.Orders
                    .Where(o => o.Status == "delivered" && o.CreatedAt.Month == currentMonth && o.CreatedAt.Year == currentYear)
                    .SumAsync(o => o.TotalAmount),

                NewUsersToday = await _context.Users
                    .CountAsync(u => u.CreatedAt >= today)
            };
        }
    }
}
