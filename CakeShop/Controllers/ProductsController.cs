using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CakeShop.Data;
using CakeShop.Models;
using CakeShop.Models.ViewModels;

namespace CakeShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public int PageSize = 9;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int productPage=1)
        {
            return View(
                new ProductListViewModel
                {
                    Products = _context.Product
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo =new PagingInfo
                    {
                        TotalItems= _context.Product.Count(),
                        ItemPerPage=PageSize,
                        CurrentPage=productPage
                    }
                }

                ) ;
        }
        [HttpPost]
        public async Task<IActionResult> Search(string keywords, int productPage = 1)
        {
            return View("Index",
                new ProductListViewModel
                {
                    Products = _context.Product
                    .Where(p=>p.title.Contains(keywords))
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        TotalItems = _context.Product.Where(p => p.title.Contains(keywords)).Count(),
                        ItemPerPage = PageSize,
                        CurrentPage = productPage
                    }
                }

                );
        }
        public IActionResult GetFilteredProducts([FromBody] FilterData filter)
        {
            var filteredProducts = _context.Product.ToList();
            if (filter.PriceRanges != null && filter.PriceRanges.Count>0 && !filter.PriceRanges.Contains("all"))
            {
                List<PriceRange> priceRanges = new List<PriceRange>();
                foreach(var range in filter.PriceRanges)
                {
                    var value = range.Split("-").ToArray();
                    PriceRange priceRange = new PriceRange();
                    priceRange.Min = Int32.Parse(value[0]);
                    priceRange.Max = Int32.Parse(value[1]);
                    priceRanges.Add(priceRange);
                }
                filteredProducts = filteredProducts.Where(p=>priceRanges.Any(r=>(p.price-p.discount_price) >= r.Min && (p.price-p.discount_price) <= r.Max)).ToList();
            }
            return PartialView("_ReturnProduct", filteredProducts);
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
            var category = await _context.Category.FirstOrDefaultAsync(m => m.category_id == product.category_id);
            ViewData["name"] =category.name;
            ViewData["id"] = category.category_id;
            return View(product);
        }
		// GET: Products/Categories/5
		public async Task<IActionResult> Category(int? id, int productPage = 1)
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

            return View("Index",
                new ProductListViewModel
                {
                    Products = _context.Product
                    .Where(p => p.category_id == id)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        TotalItems = _context.Product.Count(),
                        ItemPerPage = PageSize,
                        CurrentPage = productPage
                    }
                }
                );
        }

		private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.product_id == id)).GetValueOrDefault();
        }
    }
}
