using Microsoft.EntityFrameworkCore;
using FashionShopAPI.Data;
using FashionShopAPI.Models.Entities;
using FashionShopAPI.Repositories.Interfaces;

namespace FashionShopAPI.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FashionShopDbContext _context;

        public OrderRepository(FashionShopDbContext context)
        {
            _context = context;
        }

        public async Task<string?> PlaceOrderAsync(Order order, List<OrderDetail> details)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Orders.AddAsync(order);
                foreach (var detail in details)
                {
                    await _context.OrderDetails.AddAsync(detail);

                    var productSize = await _context.ProductSizes
                        .FirstOrDefaultAsync(ps => ps.ProductId == detail.ProductId && ps.Size == detail.Size);

                    if (productSize != null)
                    {
                        if (productSize.Stock < detail.Quantity)
                            throw new Exception($"Product {detail.ProductName} size {detail.Size} is out of stock!");

                        productSize.Stock -= detail.Quantity;
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return order.OrderId;
            }
            catch
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(string orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
    }
}
