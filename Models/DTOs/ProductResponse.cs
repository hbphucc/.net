namespace FashionShopAPI.Models.DTOs
{
    public class ProductResponse
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Img { get; set; }
        public int TotalStock { get; set; }
        public Dictionary<string, int> SizeQuantities { get; set; } = new Dictionary<string, int>();
    }
}