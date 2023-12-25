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
    }
}