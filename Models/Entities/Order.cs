using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShopAPI.Models.Entities
{
    [Table("tblOrders")]
    public class Order
    {
        [Key]
        [Column("orderID")]
        public string OrderId { get; set; } = Guid.NewGuid().ToString();

        [Column("userID")]
        public string UserId { get; set; } = string.Empty;

        [Column("totalAmount")]
        public decimal TotalAmount { get; set; }

        [Column("fullName")]
        public string FullName { get; set; } = string.Empty;

        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [Column("address")]
        public string Address { get; set; } = string.Empty;

        [Column("note")]
        public string? Note { get; set; }

        [Column("paymentMethod")]
        public string PaymentMethod { get; set; } = "COD";

        [Column("status")]
        public string Status { get; set; } = "pending";

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}