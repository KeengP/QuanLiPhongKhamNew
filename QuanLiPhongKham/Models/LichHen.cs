using System;
using System.Collections.Generic;

namespace QuanLiPhongKham.Models;

public partial class LichHen
{
    public int LichHenId { get; set; }

    public int BenhNhanId { get; set; }

    public int BacSiId { get; set; }

    public DateTime NgayHen { get; set; }

    public string? TrangThai { get; set; }

    public virtual BacSi BacSi { get; set; } = null!;

    public virtual BenhNhan BenhNhan { get; set; } = null!;

    public virtual ICollection<DonThuoc> DonThuocs { get; set; } = new List<DonThuoc>();
}
