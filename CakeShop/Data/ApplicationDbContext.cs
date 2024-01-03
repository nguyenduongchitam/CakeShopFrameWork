using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CakeShop.Models;

namespace CakeShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CakeShop.Models.Category>? Category { get; set; }
        public DbSet<CakeShop.Models.Product>? Product { get; set; }
        public DbSet<CakeShop.Models.News>? News { get; set; }
        public DbSet<CakeShop.Models.Order>? Order { get; set; }
        public DbSet<CakeShop.Models.Order_Detail>? Order_Detail { get; set; }
        public DbSet<CakeShop.Models.User>? User { get; set; }
    }
}