using CakeShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Components
{
	public class RelatedProducts:ViewComponent
	{
		private readonly ApplicationDbContext _context;

		public RelatedProducts(ApplicationDbContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			return View(_context.Product.ToList());
		}
	}
}
