using FashionShopAPI.Models.Entities;

namespace FashionShopAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<string?> PlaceOrderAsync(Order order, List<OrderDetail> details);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
    }
}