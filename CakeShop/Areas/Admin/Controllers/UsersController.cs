using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CakeShop.Data;
using CakeShop.Models;
using BCrypt.Net;
namespace CakeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            return _context.User != null ?
                        View(await _context.User.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.User'  is null.");
        }

        [Route("ThemTaiKhoan")]
        [HttpGet]
        public IActionResult ThemTaiKhoan()
        {
            return View();
        }
        [Route("ThemTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTaiKhoan(User TaiKhoan)
        {
            if (ModelState.IsValid)
            {
               string hashedPassword = BCrypt.Net.BCrypt.HashPassword(TaiKhoan.password);
                TaiKhoan.password=hashedPassword;
                DateTime currentDate = DateTime.Now;
                TaiKhoan.created_at = currentDate;
                TaiKhoan.updated_at = currentDate;
                _context.User.Add(TaiKhoan);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(TaiKhoan);
        }
        //sua danh muc san pham

        [Route("SuaTaiKhoan")]
        [HttpGet]
        public IActionResult SuaTaiKhoan(int maTaiKhoan)
        {  
            var TaiKhoan = _context.User.Find(maTaiKhoan);
            return View(TaiKhoan);
        }
        [Route("SuaTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTaiKhoan(User TaiKhoan)
        {
            if (ModelState.IsValid)
            {
                DateTime currentDate = DateTime.Now;
             
                TaiKhoan.updated_at = currentDate;
                _context.Entry(TaiKhoan).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(TaiKhoan);
        }
        [Route("XoaTaiKhoan")]
        [HttpGet]
        public IActionResult XoaTaiKhoan(int maTaiKhoan)
        {
            var TaiKhoan = _context.User.Find(maTaiKhoan);
            return View(TaiKhoan);
        }

        [Route("XacNhanXoaTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XacNhanXoaTaiKhoan(User TaiKhoan)
        {

            _context.User.Remove(TaiKhoan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("ChiTietTaiKhoan")]
        [HttpGet]
        public IActionResult ChiTietTaiKhoan(int maTaiKhoan)
        {
            var TaiKhoan = _context.User.Find(maTaiKhoan);
            return View(TaiKhoan);
        }
    }
}
