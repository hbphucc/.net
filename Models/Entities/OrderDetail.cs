using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShopAPI.Models.Entities
{
    [Table("tblOrderDetails")]
    public class OrderDetail
    {
        [Key]
        [Column("detailID")]
        public int DetailId { get; set; }

        [Column("orderID")]
        public string OrderId { get; set; } = string.Empty;

        [Column("productID")]
        public string ProductId { get; set; } = string.Empty;

        [Column("productName")]
        public string ProductName { get; set; } = string.Empty;

        [Column("size")]
        public string Size { get; set; } = string.Empty;

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        // Navigation properties
        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}