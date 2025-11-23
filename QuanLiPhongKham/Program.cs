using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLiPhongKham.Data;
using QuanLiPhongKham.Models; // ✅ thêm dòng này để gọi DbContext của phòng khám

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// 🔹 Kết nối database Identity (hệ thống tài khoản)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 🔹 Cấu hình Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// -----------------------------
// 🔹 Kết nối database chính của phòng khám
builder.Services.AddDbContext<QuanLiPhongKhamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -----------------------------
// 🔹 Cấu hình MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// -----------------------------
// 🔹 Cấu hình pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // ✅ thêm để bật hệ thống đăng nhập
app.UseAuthorization();

// -----------------------------
// 🔹 Cấu hình route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // trở về Home/Index làm trang chính
app.MapRazorPages();

app.Run();
