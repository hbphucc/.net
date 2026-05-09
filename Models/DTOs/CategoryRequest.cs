namespace FashionShopAPI.Models.DTOs
{
    public class CategoryRequest
    {
        public string CategoryName { get; set; } = string.Empty;
        public string Gender { get; set; } = "unisex";
        public string? Description { get; set; }
    }
}
