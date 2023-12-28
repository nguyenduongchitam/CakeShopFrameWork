using CakeShop.Data;
using CakeShop.Infrastructure;
using CakeShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Protocol.Core.Types;

namespace CakeShop.Controllers
{
	public class CartController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CartController(ApplicationDbContext context)
		{
			_context = context;
			//Cart = new Cart();
		}
		public Cart? Cart { get; set; }
		public IActionResult Index()
		{
			return View("Cart", HttpContext.Session.GetJson<Cart>("cart"));
		}
		public IActionResult AddToCart(int productID)
		{
			Product? product = _context.Product?
			.FirstOrDefault(p => p.product_id == productID);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, 1);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}
		public IActionResult AddMultyToCart(int productID,int qty)
		{
			Product? product = _context.Product?
			.FirstOrDefault(p => p.product_id == productID);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, qty);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}
		public IActionResult RemoveFromCart(int productID)
		{
			Product? product = _context.Product?
			.FirstOrDefault(p => p.product_id == productID);
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.RemoveLine(product);
				HttpContext.Session.SetJson("cart", Cart);
			}
			
			return View("Cart", Cart);
		}
        public IActionResult DecreaseCart(int productID)
        {
            Product? product = _context.Product?
            .FirstOrDefault(p => p.product_id == productID);
            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, -1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("Cart", Cart);
        }
		public IActionResult CheckOut()
		{
			return View(HttpContext.Session.GetJson<Cart>("cart"));
		}
    }
}
