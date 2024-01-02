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
    public class FeedBacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedBacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/FeedBacks
        public async Task<IActionResult> Index()
        {
              return _context.FeedBack != null ? 
                          View(await _context.FeedBack.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.FeedBack'  is null.");
        }

  
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FeedBack == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBack
                .FirstOrDefaultAsync(m => m.feedback_id == id);
            if (feedBack == null)
            {
                return NotFound();
            }

            return View(feedBack);
        }

       // Lưu ý , thêm phản hồi để test thôi, chừng sẽ bỏ
        [Route("ThemPhanHoi")]
        [HttpGet]
        public IActionResult ThemPhanHoi()
        {
            return View();
        }
        [Route("ThemPhanHoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemPhanHoi(FeedBack PhanHoi)
        {
            if (ModelState.IsValid)
            {
                _context.FeedBack.Add(PhanHoi);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(PhanHoi);
        }
    }
}
