using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiPhongKham.Models;

namespace QuanLiPhongKham.Controllers
{
    public class ChiTietDonThuocController : Controller
    {
        private readonly QuanLiPhongKhamContext _context;

        public ChiTietDonThuocController(QuanLiPhongKhamContext context)
        {
            _context = context;
        }

        public IActionResult Create(int donThuocId)
        {
            ViewBag.DonThuocId = donThuocId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChiTietDonThuoc model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "DonThuoc", new { id = model.DonThuocId });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.ChiTietDonThuocs.FindAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChiTietDonThuoc model)
        {
            if (!ModelState.IsValid) return View(model);

            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "DonThuoc", new { id = model.DonThuocId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ChiTietDonThuocs
                .Include(x => x.DonThuoc)
                .FirstOrDefaultAsync(x => x.ChiTietId == id);

            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.ChiTietDonThuocs.FindAsync(id);
            if (item == null) return NotFound();

            int donThuocId = item.DonThuocId;

            _context.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "DonThuoc", new { id = donThuocId });
        }
    }
}
