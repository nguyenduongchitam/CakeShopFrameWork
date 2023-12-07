using System.ComponentModel.DataAnnotations;

namespace CakeShop.Models
{
    public class News
    {
        [Key]
        public int news_id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string thumbnail { get; set; }
        public DateTime? publish_date { get; set; }
    }
}
