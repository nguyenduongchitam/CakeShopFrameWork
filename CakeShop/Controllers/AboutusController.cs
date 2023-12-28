using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Controllers
{
	public class AboutusController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
