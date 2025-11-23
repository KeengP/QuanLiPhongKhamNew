using Microsoft.AspNetCore.Mvc;
using QuanLiPhongKham.Data;
using QuanLiPhongKham.Models;
using System.Linq;

namespace QuanLiPhongKham.Controllers
{
    public class LoginController : Controller
    {
        private readonly QuanLiPhongKhamContext _context;

        public LoginController(QuanLiPhongKhamContext context)
        {
            _context = context;
        }

        // GET: /Login/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Login/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Đăng nhập thành công → chuyển về trang chủ
                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }

        // GET: /Login/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Login/Register
        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng username
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ViewBag.Error = "Tên đăng nhập đã tồn tại!";
                    return View(model);
                }

                // Lưu tài khoản mới
                _context.Users.Add(model);
                _context.SaveChanges();

                // Sau khi đăng ký → quay về trang đăng nhập
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
            return View(model);
        }

        // GET: /Login/Logout
        public IActionResult Logout()
        {
            // Có thể thêm logic clear session nếu có
            return RedirectToAction("Login", "Login");
        }
    }
}
