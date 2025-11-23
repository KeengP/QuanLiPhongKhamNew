using System;
using System.Collections.Generic;

namespace QuanLiPhongKham.Models;

public partial class BacSi
{
    public int BacSiId { get; set; }

    public string HoTen { get; set; } = null!;

    public string? ChuyenKhoa { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();
}
