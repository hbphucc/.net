namespace FashionShopAPI.Models.DTOs
{
    public class CheckoutRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Note { get; set; }
        public string PaymentMethod { get; set; } = "COD";
        public List<CartItemRequest> CartItems { get; set; } = new List<CartItemRequest>();
    }
    public class CartItemRequest
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
