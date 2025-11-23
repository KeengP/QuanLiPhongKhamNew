using System;
using System.Collections.Generic;

namespace QuanLiPhongKham.Models;

public partial class ThanhToan
{
    public int ThanhToanId { get; set; }

    public int BenhNhanId { get; set; }

    public DateTime? NgayThanhToan { get; set; }

    public decimal? TongTien { get; set; }

    public string? HinhThucThanhToan { get; set; }

    public virtual BenhNhan BenhNhan { get; set; } = null!;
}
