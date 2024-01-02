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
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public NewsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/News
        public async Task<IActionResult> Index()
        {
              return _context.News != null ? 
                          View(await _context.News.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.News'  is null.");
        }


        [Route("ThemTinTuc")]
        [HttpGet]
        public IActionResult ThemTinTuc()
        {
            return View();
        }
        [Route("ThemTinTuc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTinTuc(News TinTuc , IFormFile uploadhinh)
        {
            if (ModelState.IsValid)
            {
                if (uploadhinh != null && uploadhinh.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadhinh.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    uploadhinh.CopyTo(new FileStream(filePath, FileMode.Create));
                    TinTuc.thumbnail = uniqueFileName;
                }

                DateTime currentDate = DateTime.Now;
                TinTuc.publish_date= currentDate;
                _context.News.Add(TinTuc);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(TinTuc);
        }
        [Route("SuaTinTuc")]
        [HttpGet]
        public IActionResult SuaTinTuc(int maTinTuc)
        {
            
            var TinTuc = _context.News.Find(maTinTuc);
            return View(TinTuc);
        }

        [Route("SuaTinTuc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTinTuc(News TinTuc, IFormFile uploadhinh)
        {
            if (ModelState.IsValid)
            {
                if (uploadhinh != null && uploadhinh.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadhinh.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    uploadhinh.CopyTo(new FileStream(filePath, FileMode.Create));
                    TinTuc.thumbnail = uniqueFileName;
                }

                DateTime currentDate = DateTime.Now;

                TinTuc.publish_date = currentDate;
                _context.Entry(TinTuc).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(TinTuc);
        }
        [Route("ChiTietTinTuc")]
        [HttpGet]
        public IActionResult ChiTietTinTuc(int maTinTuc)
        {
            var TinTuc = _context.News.Find(maTinTuc);
            return View(TinTuc);
        }
        [Route("XoaTinTuc")]
        [HttpGet]
        public IActionResult XoaTinTuc(int maTinTuc)
        {
            var TinTuc = _context.News.Find(maTinTuc);
            return View(TinTuc);
        }

        [Route("XacNhanXoaTinTuc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XacNhanXoaTinTuc(News TinTuc)
        {

            _context.News.Remove(TinTuc);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
