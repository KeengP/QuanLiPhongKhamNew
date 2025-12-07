using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QuanLiPhongKham.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// Database
builder.Services.AddDbContext<QuanLiPhongKhamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClinicConnection")));

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Cookie Authentication (KHÔNG sử dụng Identity)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";             // Chưa login → chuyển vào Login
        options.AccessDeniedPath = "/User/AccessDenied"; // Không đủ quyền → AccessDenied
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication + Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
