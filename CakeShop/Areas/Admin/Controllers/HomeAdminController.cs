using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeAdminController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult quanlytaikhoan()
        {
            return View();
        }
        public IActionResult quanlydanhmucsanpham()
        {
            return View();
        }
        public IActionResult quanlysanpham()
        {
      
            return View();
        }
        public IActionResult quanlyhoadon()
        {
            return View();
        }
        public IActionResult quanlykhachhang()
        {
            return View();
        }
        public IActionResult quanlyphanhoi()
        {
            return View();
        }
        public IActionResult quanlybaocaovathongke()
        {
            return View();
        }

    }
}
