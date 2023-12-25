using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CakeShop.Data;
using CakeShop.Models;

namespace CakeShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return _context.Product != null ? 
                          View(await _context.Product.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Product'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.product_id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
		// GET: Products/Categories/5
		public async Task<IActionResult> Category(int? id)
		{
            if (id == null)
            {
                return NotFound();
            }

            // Lấy danh sách sản phẩm theo id danh mục
            var products = await _context.Product
                .Where(p => p.category_id == id)
                .ToListAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound();
            }

            return View("Index",products);
        }

		private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.product_id == id)).GetValueOrDefault();
        }
    }
}
