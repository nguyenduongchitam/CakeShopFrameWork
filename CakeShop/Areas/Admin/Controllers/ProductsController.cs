using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CakeShop.Data;
using CakeShop.Models;
using Microsoft.AspNetCore.Hosting;

namespace CakeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        [Route("ThemSanPham")]
        [HttpGet]
        public IActionResult ThemSanPham()
        {
            ViewBag.category_id = new SelectList(_context.Category.ToList(), "category_id", "name");
            return View();
        }
        [Route("ThemSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPham(Product SanPham, IFormFile uploadhinh)
        {
            if (ModelState.IsValid)
            {
                if (uploadhinh != null && uploadhinh.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string uniqueFileName = uploadhinh.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    uploadhinh.CopyTo(new FileStream(filePath, FileMode.Create));
                    SanPham.thumbnail = uniqueFileName;
                }
                DateTime currentDate = DateTime.Now;
                SanPham.created_at = currentDate;
                SanPham.updated_at = currentDate;
                _context.Product.Add(SanPham);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(SanPham);
        }
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int maSanPham)
        {
            ViewBag.category_id = new SelectList(_context.Category.ToList(), "category_id", "name");
            var SanPham = _context.Product.Find(maSanPham);
            return View(SanPham);
        }

        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(Product SanPham, IFormFile uploadhinh)
        {
            if (ModelState.IsValid)
            {
                if (uploadhinh != null && uploadhinh.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadhinh.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    uploadhinh.CopyTo(new FileStream(filePath, FileMode.Create));
                    SanPham.thumbnail = uniqueFileName;
                }

                DateTime currentDate = DateTime.Now;

                SanPham.updated_at = currentDate;
                _context.Entry(SanPham).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(SanPham);
        }
        [Route("ChiTietSanPham")]
        [HttpGet]
        public IActionResult ChiTietSanPham(int maSanPham)
        {
            var SanPham = _context.Product.Find(maSanPham);
            return View(SanPham);
        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(int maSanPham)
        {
            var SanPham = _context.Product.Find(maSanPham);
            return View(SanPham);
        }

        [Route("XacNhanXoaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XacNhanXoaSanPham(Product SanPham)
        {

            _context.Product.Remove(SanPham);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}