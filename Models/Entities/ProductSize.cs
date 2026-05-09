using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShopAPI.Models.Entities
{
    [Table("tblProductSizes")]
    public class ProductSize
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("productID")]
        public string ProductId { get; set; } = string.Empty;

        [Column("size")]
        public string Size { get; set; } = string.Empty;

        [Column("stock")]
        public int Stock { get; set; } = 0;

        // Navigation property
        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}