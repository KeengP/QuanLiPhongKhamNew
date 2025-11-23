using Microsoft.AspNetCore.Mvc;
using QuanLiPhongKham.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLiPhongKham.Controllers
{
    public class BacSiController : Controller
    {
        private readonly QuanLiPhongKhamContext _context;

        public BacSiController(QuanLiPhongKhamContext context)
        {
            _context = context;
        }

        // TRANG DANH SÁCH BÁC SĨ
        public async Task<IActionResult> Index()
        {
            var danhSachBacSi = await _context.BacSis.ToListAsync();
            return View(danhSachBacSi);
        }

        // TRANG CHI TIẾT
        public async Task<IActionResult> Details(int id)
        {
            var bacSi = await _context.BacSis.FirstOrDefaultAsync(b => b.BacSiId == id);
            if (bacSi == null)
                return NotFound();

            return View(bacSi);
        }

        // FORM THÊM
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // XỬ LÝ THÊM
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BacSi bacSi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bacSi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bacSi);
        }

        // FORM SỬA
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var bacSi = await _context.BacSis.FindAsync(id);

            if (bacSi == null)
                return NotFound();

            return View(bacSi);
        }

        // XỬ LÝ SỬA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BacSi bacSi)
        {
            if (id != bacSi.BacSiId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bacSi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.BacSis.Any(e => e.BacSiId == id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(bacSi);
        }

        // TRANG XÁC NHẬN XOÁ
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var bacSi = await _context.BacSis.FirstOrDefaultAsync(b => b.BacSiId == id);

            if (bacSi == null)
                return NotFound();

            return View(bacSi);
        }

        // XỬ LÝ XOÁ THẬT SỰ
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bacSi = await _context.BacSis.FindAsync(id);

            if (bacSi != null)
            {
                _context.BacSis.Remove(bacSi);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // QUAY LẠI TRANG CHỦ
        public IActionResult Home()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
