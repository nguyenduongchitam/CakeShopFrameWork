using CakeShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CakeShop.Components
{
    public class Bestseller :ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Bestseller(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
			
			return View(_context.Product.ToList());
        }
    }
}
