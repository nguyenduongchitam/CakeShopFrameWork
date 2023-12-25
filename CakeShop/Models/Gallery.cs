using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeShop.Models
{
    public class Gallery
    {
        [Key]
        public string gallery_id { get; set; }
        [ForeignKey("Product")]
        public string product_id { get; set; }
        public string thumbnail {  get; set; }

    }
}
