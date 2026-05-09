using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShopAPI.Models.Entities
{
    [Table("tblUsers")]
    public class User
    {
        [Key]
        [Column("userID")]
        public string UserId { get; set; } = Guid.NewGuid().ToString();

        [Column("fullName")]
        public string FullName { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("role")]
        public string Role { get; set; } = "customer";

        [Column("status")]
        public bool Status { get; set; } = true;

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
