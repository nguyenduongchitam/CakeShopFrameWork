using System.ComponentModel.DataAnnotations;

namespace CakeShop.Models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }
        public string name { get; set; }
    }
}
