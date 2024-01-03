using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeShop.Models
{
    public class Order
    {
        [Key]
        public int order_id { get; set; }
        [ForeignKey("User")]
        public int user_id { get; set; }
        [StringLength(255)]
        public string note { get; set;}
        [StringLength(60)]
        public string city { get; set;}
        [StringLength(60)]
        public string district { get; set;}
        [StringLength(60)]
        public string ward { get; set;}
        public string address { get; set;}
        public int delivery_money { get; set;}
        public int discount { get; set;}
        public DateTime? created_at { get; set; } = default(DateTime?);
        public int total_money { get; set; }
        public string status {  get; set; }

    }
}
