using CakeShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Components
{
    public class Newin:ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Newin(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Product.ToList());
        }
    }
}
