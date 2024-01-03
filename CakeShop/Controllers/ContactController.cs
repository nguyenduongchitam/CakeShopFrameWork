using CakeShop.Data;
using CakeShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CakeShop.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(FeedBack feedback)
        {
            if (ModelState.IsValid)
            {
                // Lưu dữ liệu vào cơ sở dữ liệu
                _context.FeedBack.Add(feedback);
                _context.SaveChanges();

                // Redirect hoặc hiển thị thông báo thành công
                return RedirectToAction("Index");
            }

            // Nếu dữ liệu không hợp lệ, quay trở lại trang Index để hiển thị lại form
            return View("Index", feedback);
        }
    }
}
