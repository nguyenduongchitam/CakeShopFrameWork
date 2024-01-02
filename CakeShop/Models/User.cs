using System.ComponentModel.DataAnnotations;

namespace CakeShop.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        [StringLength(50)]
        public string full_name { get; set;}
        [StringLength(50)]
        public string email { get; set; }
        [StringLength(50)]
        public string password { get; set; }
        [StringLength(255)]
        public string role { get; set; }
        [StringLength(50)]
        public string phone_number { get; set;}
        public DateTime? created_at { get; set; } = default(DateTime?);
        public DateTime? updated_at { get;set; }    
    }
}
