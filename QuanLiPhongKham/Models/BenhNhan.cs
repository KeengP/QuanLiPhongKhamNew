using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLiPhongKham.Models;

public partial class BenhNhan
{
    public int BenhNhanId { get; set; }

    public string HoTen { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? DiaChi { get; set; }

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải gồm đúng 10 chữ số")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có 10 số")]
    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
