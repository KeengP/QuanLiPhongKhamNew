using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLiPhongKham.Models
{
    public partial class ChiTietDonThuoc
    {
        [Key]
        public int ChiTietId { get; set; }

        public int DonThuocId { get; set; }
        public string? TenThuoc { get; set; }
        public string? LieuLuong { get; set; }
        public int? SoLuong { get; set; }

        public virtual DonThuoc DonThuoc { get; set; } = null!;
    }
}
