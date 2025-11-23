using System;
using System.Collections.Generic;

namespace QuanLiPhongKham.Models;

public partial class DonThuoc
{
    public int DonThuocId { get; set; }

    public int LichHenId { get; set; }

    public DateTime? NgayLap { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietDonThuoc> ChiTietDonThuocs { get; set; } = new List<ChiTietDonThuoc>();

    public virtual LichHen LichHen { get; set; } = null!;
}
