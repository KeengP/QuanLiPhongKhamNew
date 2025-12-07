using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLiPhongKham.Models;

public class ThongKeController : Controller
{
    private readonly QuanLiPhongKhamContext _context;

    public ThongKeController(QuanLiPhongKhamContext context)
    {
        _context = context;
    }

    // Thống kê doanh thu theo tháng của 1 năm
    public async Task<IActionResult> DoanhThuThang(int? year)
    {
        int nam = year ?? DateTime.Now.Year;

        var data = await _context.ThanhToans
            .Where(t => t.NgayThanhToan.HasValue && t.NgayThanhToan.Value.Year == nam)
            .GroupBy(t => t.NgayThanhToan.Value.Month)
            .Select(g => new DoanhThuViewModel
            {
                Label = "Tháng " + g.Key,
                Value = g.Sum(x => x.TongTien ?? 0)
            })
            .OrderBy(x => x.Label)
            .ToListAsync();

        ViewBag.Year = nam;
        return View(data);
    }

    // Thống kê tổng doanh thu theo năm
    public async Task<IActionResult> DoanhThuNam()
    {
        var data = await _context.ThanhToans
            .Where(t => t.NgayThanhToan.HasValue)
            .GroupBy(t => t.NgayThanhToan.Value.Year)
            .Select(g => new DoanhThuViewModel
            {
                Label = "Năm " + g.Key,
                Value = g.Sum(x => x.TongTien ?? 0)
            })
            .OrderBy(x => x.Label)
            .ToListAsync();

        return View(data);
    }
}

public class DoanhThuViewModel
{
    public string Label { get; set; }
    public decimal Value { get; set; }
}
