using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionShopAPI.Models.Entities
{
    [Table("tblCategories")]
    public class Category
    {
        [Key]
        [Column("categoryID")]
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();

        [Column("categoryName")]
        public string CategoryName { get; set; } = string.Empty;

        [Column("gender")]
        public string Gender { get; set; } = "unisex";

        [Column("description")]
        public string? Description { get; set; }

        [Column("status")]
        public bool Status { get; set; } = true;

        // Navigation property
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
