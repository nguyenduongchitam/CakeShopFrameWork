using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CakeShop.Data;
using CakeShop.Models;

namespace CakeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
              return _context.Category != null ? 
                          View(await _context.Category.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Category'  is null.");
        }
        [Route("ThemDanhMucSanPham")]
        [HttpGet]
        public IActionResult ThemDanhMucSanPham()
        {
            return View();
        }
        [Route("ThemDanhMucSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemDanhMucSanPham(Category DanhMucSanPham)
        {
            if (ModelState.IsValid)
            {
               
                _context.Category.Add(DanhMucSanPham);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(DanhMucSanPham);
        }
        //sua danh muc san pham

        [Route("SuaDanhMucSanPham")]
        [HttpGet]
        public IActionResult SuaDanhMucSanPham(int maDanhMucSanPham)
        {
            var DanhMucSanPham = _context.Category.Find(maDanhMucSanPham);
            return View(DanhMucSanPham);
        }
        [Route("SuaDanhMucSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaDanhMucSanPham(Category DanhMucSanPham)
        {
            if (ModelState.IsValid)
            {
              
                _context.Entry(DanhMucSanPham).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(DanhMucSanPham);
        }
        [Route("XoaDanhMucSanPham")]
        [HttpGet]
        public IActionResult XoaDanhMucSanPham(int maDanhMucSanPham)
        {
            var DanhMucSanPham = _context.Category.Find(maDanhMucSanPham);
            return View(DanhMucSanPham);
        }

        [Route("XacNhanXoaDanhMucSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XacNhanXoaDanhMucSanPham(Category DanhMucSanPham)
        {

            _context.Category.Remove(DanhMucSanPham);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("ChiTietDanhMucSanPham")]
        [HttpGet]
        public IActionResult ChiTietDanhMucSanPham(int maDanhMucSanPham)
        {
            var DanhMucSanPham = _context.Category.Find(maDanhMucSanPham);
            return View(DanhMucSanPham);
        }

    }
}
