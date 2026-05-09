using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShopAPI.Models.Entities
{
    [Table("tblClothes")]
    public class Product
    {
        [Key]
        [Column("productID")]
        public string ProductId { get; set; } = Guid.NewGuid().ToString();

        [Column("productName")]
        public string ProductName { get; set; } = string.Empty;

        [Column("categoryID")]
        public string CategoryId { get; set; } = string.Empty;

        [Column("price")]
        public decimal Price { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("img")]
        public string? Img { get; set; }

        [Column("status")]
        public bool Status { get; set; } = true;

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;

        public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
    }
}