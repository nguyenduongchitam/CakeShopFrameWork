using System.ComponentModel.DataAnnotations;

namespace CakeShop.Models
{
    public class FeedBack
    {
        [Key]
        public int feedback_id {  get; set; }
        [StringLength(50)]
        public string firstname { get; set; }
        [StringLength(50)]
        public string lastname { get; set; }
        [StringLength(50)]
        public string email { get; set; }
        [StringLength(50)]
        public string phone_number { get; set; }
        [StringLength(255)]
        public string note { get; set; }
    }
}
