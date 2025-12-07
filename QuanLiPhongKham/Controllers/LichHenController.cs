using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiPhongKham.Models;

namespace QuanLiPhongKham.Controllers
{
    [Authorize]
    public class LichHenController : Controller
    {
        private readonly QuanLiPhongKhamContext _context;

        public LichHenController(QuanLiPhongKhamContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ds = await _context.LichHens
                .Include(x => x.BacSi)
                .Include(x => x.BenhNhan)
                .ToListAsync();

            return View(ds);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.LichHens
                .Include(x => x.BacSi)
                .Include(x => x.BenhNhan)
                .FirstOrDefaultAsync(x => x.LichHenId == id);

            if (item == null) return NotFound();
            return View(item);
        }

        [Authorize(Roles = "Admin,LeTan")]
        public IActionResult Create()
        {
            LoadDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,LeTan")]
        public async Task<IActionResult> Create(LichHen model)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdown();
                return View(model);
            }

            if (string.IsNullOrEmpty(model.TrangThai))
                model.TrangThai = "Mở";

            _context.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,LeTan")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.LichHens.FindAsync(id);
            if (item == null) return NotFound();

            LoadDropdown();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,LeTan")]
        public async Task<IActionResult> Edit(LichHen model)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdown();
                return View(model);
            }

            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,LeTan")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.LichHens
                .Include(x => x.BacSi)
                .Include(x => x.BenhNhan)
                .FirstOrDefaultAsync(x => x.LichHenId == id);

            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,LeTan")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.LichHens.FindAsync(id);

            if (item != null)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadDropdown()
        {
            ViewBag.BacSi = _context.BacSis.ToList();
            ViewBag.BenhNhan = _context.BenhNhans.ToList();
        }
    }
}
