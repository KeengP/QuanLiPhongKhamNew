using Microsoft.AspNetCore.Mvc;
using QuanLiPhongKham.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLiPhongKham.Controllers
{
    public class BenhNhanController : Controller
    {
        private readonly QuanLiPhongKhamContext _context;

        public BenhNhanController(QuanLiPhongKhamContext context)
        {
            _context = context;
        }

        // GET: BenhNhan
        public async Task<IActionResult> Index()
        {
            return View(await _context.BenhNhans.ToListAsync());
        }

        // GET: BenhNhan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var benhNhan = await _context.BenhNhans
                .FirstOrDefaultAsync(m => m.BenhNhanId == id);

            if (benhNhan == null)
                return NotFound();

            return View(benhNhan);
        }

        // GET: BenhNhan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BenhNhan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BenhNhan benhNhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(benhNhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(benhNhan);
        }

        // GET: BenhNhan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var benhNhan = await _context.BenhNhans.FindAsync(id);

            if (benhNhan == null)
                return NotFound();

            return View(benhNhan);
        }

        // POST: BenhNhan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BenhNhan benhNhan)
        {
            if (id != benhNhan.BenhNhanId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(benhNhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(benhNhan);
        }

        // GET: BenhNhan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var benhNhan = await _context.BenhNhans
                .FirstOrDefaultAsync(m => m.BenhNhanId == id);

            if (benhNhan == null)
                return NotFound();

            return View(benhNhan);
        }

        // POST: BenhNhan/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var benhNhan = await _context.BenhNhans.FindAsync(id);
            if (benhNhan != null)
            {
                _context.BenhNhans.Remove(benhNhan);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
