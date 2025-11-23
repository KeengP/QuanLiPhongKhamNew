using System;
using System.Collections.Generic;

namespace QuanLiPhongKham.Models;

public partial class BenhNhan
{
    public int BenhNhanId { get; set; }

    public string HoTen { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
