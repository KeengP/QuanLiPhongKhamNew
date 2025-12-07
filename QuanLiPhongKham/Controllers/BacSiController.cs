using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLiPhongKham.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLiPhongKham.Controllers
{
    [Authorize]   // phải đăng nhập mới xem
    public class BacSiController : Controller
    {
        private readonly QuanLiPhongKhamContext _context;

        public BacSiController(QuanLiPhongKhamContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.BacSis.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var bacSi = await _context.BacSis.FirstOrDefaultAsync(b => b.BacSiId == id);
            if (bacSi == null) return NotFound();
            return View(bacSi);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(BacSi bacSi)
        {
            if (!ModelState.IsValid) return View(bacSi);

            _context.Add(bacSi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var bacSi = await _context.BacSis.FindAsync(id);
            if (bacSi == null) return NotFound();

            return View(bacSi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, BacSi bacSi)
        {
            if (id != bacSi.BacSiId) return NotFound();
            if (!ModelState.IsValid) return View(bacSi);

            _context.Update(bacSi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var bacSi = await _context.BacSis.FirstOrDefaultAsync(b => b.BacSiId == id);
            if (bacSi == null) return NotFound();
            return View(bacSi);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bacSi = await _context.BacSis.FindAsync(id);

            if (bacSi != null)
            {
                _context.Remove(bacSi);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
