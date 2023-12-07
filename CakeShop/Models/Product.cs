using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeShop.Models
{
    public class Product
    {
        [Key]
        public int product_id { get; set; }
        [ForeignKey("Category")]
        public int categorory_id { get; set; }
        [StringLength(255)]
        public string title { get; set; }
        public int price { get; set; }
        public int discount_price { get; set; }
        [StringLength(255)]
        public string description { get; set; }
        [StringLength(255)]
        public string thumbnail { get; set; }
        public DateTime? created_at { get; set; } = default(DateTime?);
        public DateTime? updated_at { get; set ; }
    }
}
