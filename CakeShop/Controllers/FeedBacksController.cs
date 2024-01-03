using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CakeShop.Data;
using CakeShop.Models;

namespace CakeShop.Controllers
{
    public class FeedBacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedBacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FeedBacks
        public async Task<IActionResult> Index()
        {
              return _context.FeedBack != null ? 
                          View(await _context.FeedBack.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.FeedBack'  is null.");
        }

        // GET: FeedBacks/Details/5
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

        // GET: FeedBacks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FeedBacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("feedback_id,firstname,lastname,email,phone_number,note")] FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedBack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feedBack);
        }

        // GET: FeedBacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FeedBack == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBack.FindAsync(id);
            if (feedBack == null)
            {
                return NotFound();
            }
            return View(feedBack);
        }

        // POST: FeedBacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("feedback_id,firstname,lastname,email,phone_number,note")] FeedBack feedBack)
        {
            if (id != feedBack.feedback_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedBack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedBackExists(feedBack.feedback_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(feedBack);
        }

        // GET: FeedBacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: FeedBacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FeedBack == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FeedBack'  is null.");
            }
            var feedBack = await _context.FeedBack.FindAsync(id);
            if (feedBack != null)
            {
                _context.FeedBack.Remove(feedBack);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedBackExists(int id)
        {
          return (_context.FeedBack?.Any(e => e.feedback_id == id)).GetValueOrDefault();
        }
    }
}
