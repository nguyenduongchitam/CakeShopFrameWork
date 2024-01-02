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
            return Redirect("/Admin/Users/Index");
        }
        public IActionResult quanlydanhmucsanpham()
        {
            return Redirect("/Admin/Categories/Index");
        }
        public IActionResult quanlysanpham()
        {
            return Redirect("Admin/Products/index");
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
            return Redirect("/Admin/FeedBacks/Index");
        }
        public IActionResult quanlybaocaovathongke()
        {
            return View();
        }
        public IActionResult quanlytintuc()
        {
            return Redirect("/Admin/News/Index");
        }
    }
}
