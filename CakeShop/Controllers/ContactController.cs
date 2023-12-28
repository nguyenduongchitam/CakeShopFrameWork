using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
