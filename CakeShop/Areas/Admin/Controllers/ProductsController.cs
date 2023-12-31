using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CakeShop.Data;
using CakeShop.Models;
using System.Security.Claims;

namespace CakeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
              return _context.Product != null ? 
                          View(await _context.Product.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Product'  is null.");
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.product_id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("product_id,category_id,title,price,discount_price,description,thumbnail,created_at,updated_at")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("product_id,category_id,title,price,discount_price,description,thumbnail,created_at,updated_at")] Product product)
        //{
        //    if (id != product.product_id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var productEdit = _context.Product.Find(id);
        //        productEdit.title = Utilities.ToTitleCase(product.title);
        //        productEdit.product_id = product.product_id;
        //        productEdit.price = product.price;
        //        productEdit.discount_price = product.discount_price;
        //        productEdit.thumbnail = product.thumbnail;
        //        productEdit.Ram = product.Ram;
        //        productEdit.BrandId = product.BrandId;
        //        productEdit.CategoryId = product.CategoryId;
        //        productEdit.Description = product.Description;
        //        if (fThumb != null)
        //        {
        //            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "product", product.Image);
        //            if (System.IO.File.Exists(path))
        //            {
        //                System.IO.File.Delete(path);
        //            }
        //            string extension = Path.GetExtension(fThumb.FileName);
        //            productEdit.Image = Utilities.SEOUrl(productEdit.Name) + $"-{productEdit.Id}" + extension;
        //            await Utilities.UploadFile(fThumb, @"product", productEdit.Image);

        //        }
        //        productEdit.UpdateUserId = User.FindFirstValue(Const.ADMINIDSESSION).ToString();
        //        productEdit.UpdatedAt = DateTime.Now;
        //        _context.Products.Update(productEdit);
        //        await _context.SaveChangesAsync();
        //        _notyfService.Success("Cập nhật sản phẩm thành công");
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(product);
        //}

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.product_id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.product_id == id)).GetValueOrDefault();
        }
    }
}
