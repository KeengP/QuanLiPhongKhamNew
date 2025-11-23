using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLiPhongKham.Models;

public partial class QuanLiPhongKhamContext : DbContext
{
    public QuanLiPhongKhamContext()
    {
    }

    public QuanLiPhongKhamContext(DbContextOptions<QuanLiPhongKhamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BacSi> BacSis { get; set; }

    public virtual DbSet<BenhNhan> BenhNhans { get; set; }

    public virtual DbSet<ChiTietDonThuoc> ChiTietDonThuocs { get; set; }

    public virtual DbSet<DonThuoc> DonThuocs { get; set; }

    public virtual DbSet<LichHen> LichHens { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    public DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HINA\\SQLEXPRESS01;Database=QuanLiPhongKham;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BacSi>(entity =>
        {
            entity.HasKey(e => e.BacSiId).HasName("PK__BacSi__5B2D075426589883");

            entity.ToTable("BacSi");

            entity.Property(e => e.BacSiId).HasColumnName("BacSiID");
            entity.Property(e => e.ChuyenKhoa).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
        });

        modelBuilder.Entity<BenhNhan>(entity =>
        {
            entity.HasKey(e => e.BenhNhanId).HasName("PK__BenhNhan__151050A6BAF493D5");

            entity.ToTable("BenhNhan");

            entity.Property(e => e.BenhNhanId).HasColumnName("BenhNhanID");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
        });

        modelBuilder.Entity<ChiTietDonThuoc>(entity =>
        {
            entity.HasKey(e => e.ChiTietId).HasName("PK__ChiTietD__B117E9EAFD6AE700");

            entity.ToTable("ChiTietDonThuoc");

            entity.Property(e => e.ChiTietId).HasColumnName("ChiTietID");
            entity.Property(e => e.DonThuocId).HasColumnName("DonThuocID");
            entity.Property(e => e.LieuLuong).HasMaxLength(100);
            entity.Property(e => e.TenThuoc).HasMaxLength(100);

            entity.HasOne(d => d.DonThuoc).WithMany(p => p.ChiTietDonThuocs)
                .HasForeignKey(d => d.DonThuocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDo__DonTh__5535A963");
        });

        modelBuilder.Entity<DonThuoc>(entity =>
        {
            entity.HasKey(e => e.DonThuocId).HasName("PK__DonThuoc__C8A3B4B0234CCB1F");

            entity.ToTable("DonThuoc");

            entity.Property(e => e.DonThuocId).HasColumnName("DonThuocID");
            entity.Property(e => e.GhiChu).HasMaxLength(200);
            entity.Property(e => e.LichHenId).HasColumnName("LichHenID");
            entity.Property(e => e.NgayLap)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.LichHen).WithMany(p => p.DonThuocs)
                .HasForeignKey(d => d.LichHenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonThuoc__LichHe__52593CB8");
        });

        modelBuilder.Entity<LichHen>(entity =>
        {
            entity.HasKey(e => e.LichHenId).HasName("PK__LichHen__F92991B6A9C2395B");

            entity.ToTable("LichHen");

            entity.Property(e => e.LichHenId).HasColumnName("LichHenID");
            entity.Property(e => e.BacSiId).HasColumnName("BacSiID");
            entity.Property(e => e.BenhNhanId).HasColumnName("BenhNhanID");
            entity.Property(e => e.NgayHen).HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.BacSi).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.BacSiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichHen__BacSiID__4E88ABD4");

            entity.HasOne(d => d.BenhNhan).WithMany(p => p.LichHens)
                .HasForeignKey(d => d.BenhNhanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LichHen__BenhNha__4D94879B");
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.ThanhToanId).HasName("PK__ThanhToa__24A8D6841A3C0A43");

            entity.ToTable("ThanhToan");

            entity.Property(e => e.ThanhToanId).HasColumnName("ThanhToanID");
            entity.Property(e => e.BenhNhanId).HasColumnName("BenhNhanID");
            entity.Property(e => e.HinhThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.NgayThanhToan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.BenhNhan).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.BenhNhanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ThanhToan__BenhN__59063A47");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
