namespace CakeShop.Models
{
    public class CheckOut
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string Note { get; set; }
        public string City { get; set; }
        public decimal? Discount { get; set; } = 0;
    }
}
