using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FashionShopAPI.Models.DTOs;
using FashionShopAPI.Models.Entities;
using FashionShopAPI.Repositories.Interfaces;

namespace FashionShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo; 

        public OrdersController(IOrderRepository orderRepo, IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            if (request.CartItems == null || !request.CartItems.Any())
                return BadRequest(new { message = "Cart is empty!" });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            decimal totalAmount = 0;
            var orderDetails = new List<OrderDetail>();
            string newOrderId = Guid.NewGuid().ToString();

            foreach (var item in request.CartItems)
            {
                var realProduct = await _productRepo.GetByIdAsync(item.ProductId);
                if (realProduct == null)
                    return BadRequest(new { message = $"Product {item.ProductName} does not exist." });

                totalAmount += realProduct.Price * item.Quantity;

                orderDetails.Add(new OrderDetail
                {
                    OrderId = newOrderId,
                    ProductId = item.ProductId,
                    ProductName = realProduct.ProductName, 
                    Size = item.Size,
                    Quantity = item.Quantity,
                    Price = realProduct.Price 
                });
            }

            var newOrder = new Order
            {
                OrderId = newOrderId,
                UserId = userId!,
                TotalAmount = totalAmount, 
                FullName = request.FullName,
                Phone = request.Phone,
                Address = request.Address,
                Note = request.Note,
                PaymentMethod = request.PaymentMethod,
                Status = "pending",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var orderId = await _orderRepo.PlaceOrderAsync(newOrder, orderDetails);

            if (orderId == null)
                return BadRequest(new { message = "An error occurred while placing the order, possibly due to insufficient stock!" });

            return Ok(new { message = "Order placed successfully!", orderId = orderId });
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderRepo.GetOrdersByUserIdAsync(userId!);
            return Ok(orders);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepo.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role != "admin" && order.UserId != userId)
            {
                return Forbid();
            }

            return Ok(order);
        }
    }
}