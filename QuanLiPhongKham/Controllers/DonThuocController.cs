using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiPhongKham.Models;

namespace QuanLiPhongKham.Controllers
{
    [Authorize]
    public class DonThuocController : Controller
    {
        private readonly QuanLiPhongKhamContext _context;

        public DonThuocController(QuanLiPhongKhamContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ds = await _context.DonThuocs
                .Include(x => x.LichHen).ThenInclude(x => x.BenhNhan)
                .Include(x => x.LichHen).ThenInclude(x => x.BacSi)
                .ToListAsync();

            return View(ds);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.DonThuocs
                .Include(x => x.LichHen).ThenInclude(x => x.BenhNhan)
                .Include(x => x.LichHen).ThenInclude(x => x.BacSi)
                .Include(x => x.ChiTietDonThuocs)
                .FirstOrDefaultAsync(x => x.DonThuocId == id);

            if (item == null) return NotFound();
            return View(item);
        }

        [Authorize(Roles = "Admin,BacSi")]
        public IActionResult Create()
        {
            ViewBag.LichHen = _context.LichHens
                .Include(x => x.BenhNhan)
                .Include(x => x.BacSi)
                .ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,BacSi")]
        public async Task<IActionResult> Create(DonThuoc model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.LichHen = _context.LichHens.ToList();
                return View(model);
            }

            model.NgayLap = DateTime.Now;
            _context.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,BacSi")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.DonThuocs.FindAsync(id);
            if (item == null) return NotFound();

            ViewBag.LichHen = _context.LichHens
                .Include(x => x.BenhNhan)
                .Include(x => x.BacSi)
                .ToList();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,BacSi")]
        public async Task<IActionResult> Edit(DonThuoc model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.LichHen = _context.LichHens.ToList();
                return View(model);
            }

            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,BacSi")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.DonThuocs
                .Include(x => x.LichHen).ThenInclude(x => x.BenhNhan)
                .Include(x => x.LichHen).ThenInclude(x => x.BacSi)
                .FirstOrDefaultAsync(x => x.DonThuocId == id);

            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,BacSi")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.DonThuocs.FindAsync(id);

            if (item != null)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
