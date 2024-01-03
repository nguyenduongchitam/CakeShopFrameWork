using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeShop.Models
{
    public class Order_Detail
    {
        [Key]
        public int order_id {  get; set; }
        [ForeignKey("Product")]
        public int product_id { get; set; }
        public int price { get; set; }
        public int num { get; set; }
    }
}
