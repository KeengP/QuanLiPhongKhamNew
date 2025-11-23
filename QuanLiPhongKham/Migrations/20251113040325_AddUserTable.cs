using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiPhongKham.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BacSi",
                columns: table => new
                {
                    BacSiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChuyenKhoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BacSi__5B2D075426589883", x => x.BacSiID);
                });

            migrationBuilder.CreateTable(
                name: "BenhNhan",
                columns: table => new
                {
                    BenhNhanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateOnly>(type: "date", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BenhNhan__151050A6BAF493D5", x => x.BenhNhanID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LichHen",
                columns: table => new
                {
                    LichHenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BenhNhanID = table.Column<int>(type: "int", nullable: false),
                    BacSiID = table.Column<int>(type: "int", nullable: false),
                    NgayHen = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LichHen__F92991B6A9C2395B", x => x.LichHenID);
                    table.ForeignKey(
                        name: "FK__LichHen__BacSiID__4E88ABD4",
                        column: x => x.BacSiID,
                        principalTable: "BacSi",
                        principalColumn: "BacSiID");
                    table.ForeignKey(
                        name: "FK__LichHen__BenhNha__4D94879B",
                        column: x => x.BenhNhanID,
                        principalTable: "BenhNhan",
                        principalColumn: "BenhNhanID");
                });

            migrationBuilder.CreateTable(
                name: "ThanhToan",
                columns: table => new
                {
                    ThanhToanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BenhNhanID = table.Column<int>(type: "int", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HinhThucThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThanhToa__24A8D6841A3C0A43", x => x.ThanhToanID);
                    table.ForeignKey(
                        name: "FK__ThanhToan__BenhN__59063A47",
                        column: x => x.BenhNhanID,
                        principalTable: "BenhNhan",
                        principalColumn: "BenhNhanID");
                });

            migrationBuilder.CreateTable(
                name: "DonThuoc",
                columns: table => new
                {
                    DonThuocID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LichHenID = table.Column<int>(type: "int", nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DonThuoc__C8A3B4B0234CCB1F", x => x.DonThuocID);
                    table.ForeignKey(
                        name: "FK__DonThuoc__LichHe__52593CB8",
                        column: x => x.LichHenID,
                        principalTable: "LichHen",
                        principalColumn: "LichHenID");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonThuoc",
                columns: table => new
                {
                    ChiTietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonThuocID = table.Column<int>(type: "int", nullable: false),
                    TenThuoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LieuLuong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__B117E9EAFD6AE700", x => x.ChiTietID);
                    table.ForeignKey(
                        name: "FK__ChiTietDo__DonTh__5535A963",
                        column: x => x.DonThuocID,
                        principalTable: "DonThuoc",
                        principalColumn: "DonThuocID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonThuoc_DonThuocID",
                table: "ChiTietDonThuoc",
                column: "DonThuocID");

            migrationBuilder.CreateIndex(
                name: "IX_DonThuoc_LichHenID",
                table: "DonThuoc",
                column: "LichHenID");

            migrationBuilder.CreateIndex(
                name: "IX_LichHen_BacSiID",
                table: "LichHen",
                column: "BacSiID");

            migrationBuilder.CreateIndex(
                name: "IX_LichHen_BenhNhanID",
                table: "LichHen",
                column: "BenhNhanID");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_BenhNhanID",
                table: "ThanhToan",
                column: "BenhNhanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonThuoc");

            migrationBuilder.DropTable(
                name: "ThanhToan");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DonThuoc");

            migrationBuilder.DropTable(
                name: "LichHen");

            migrationBuilder.DropTable(
                name: "BacSi");

            migrationBuilder.DropTable(
                name: "BenhNhan");
        }
    }
}
