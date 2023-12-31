using Microsoft.AspNetCore.Mvc;
using CakeShop.Models;

namespace CakeShop.Controllers
{
    public class OrderController : Controller
    {
        public ViewResult Checkout() => View(new Order());
    }
}
