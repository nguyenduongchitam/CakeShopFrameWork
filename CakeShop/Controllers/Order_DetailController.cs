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
    public class Order_DetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Order_DetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order_Detail
        public async Task<IActionResult> Index()
        {
              return _context.Order_Detail != null ? 
                          View(await _context.Order_Detail.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Order_Detail'  is null.");
        }

        // GET: Order_Detail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order_Detail == null)
            {
                return NotFound();
            }

            var order_Detail = await _context.Order_Detail
                .FirstOrDefaultAsync(m => m.order_id == id);
            if (order_Detail == null)
            {
                return NotFound();
            }

            return View(order_Detail);
        }

        // GET: Order_Detail/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Order_Detail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("order_id,product_id,price,num")] Order_Detail order_Detail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order_Detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order_Detail);
        }

        // GET: Order_Detail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order_Detail == null)
            {
                return NotFound();
            }

            var order_Detail = await _context.Order_Detail.FindAsync(id);
            if (order_Detail == null)
            {
                return NotFound();
            }
            return View(order_Detail);
        }

        // POST: Order_Detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("order_id,product_id,price,num")] Order_Detail order_Detail)
        {
            if (id != order_Detail.order_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order_Detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Order_DetailExists(order_Detail.order_id))
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
            return View(order_Detail);
        }

        // GET: Order_Detail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order_Detail == null)
            {
                return NotFound();
            }

            var order_Detail = await _context.Order_Detail
                .FirstOrDefaultAsync(m => m.order_id == id);
            if (order_Detail == null)
            {
                return NotFound();
            }

            return View(order_Detail);
        }

        // POST: Order_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order_Detail == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order_Detail'  is null.");
            }
            var order_Detail = await _context.Order_Detail.FindAsync(id);
            if (order_Detail != null)
            {
                _context.Order_Detail.Remove(order_Detail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Order_DetailExists(int id)
        {
          return (_context.Order_Detail?.Any(e => e.order_id == id)).GetValueOrDefault();
        }
    }
}
