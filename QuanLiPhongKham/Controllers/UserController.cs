using Microsoft.AspNetCore.Mvc;
using QuanLiPhongKham.Models;

namespace QuanLiPhongKham.Controllers
{
    public class UserController : Controller
    {
        private readonly QuanLiPhongKhamContext _context;

        public UserController(QuanLiPhongKhamContext context)
        {
            _context = context;
        }

        // GET: /User/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /User/Register
        [HttpPost]
        public IActionResult Register(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kiểm tra trùng username
            var existsUser = _context.Users.FirstOrDefault(x => x.Username == model.Username);
            if (existsUser != null)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại!";
                return View(model);
            }

            // Kiểm tra trùng email
            var existsEmail = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (existsEmail != null)
            {
                ViewBag.Error = "Email đã được sử dụng!";
                return View(model);
            }

            // Lưu user
            _context.Users.Add(model);
            _context.SaveChanges();

            // Chuyển về trang đăng nhập
            return RedirectToAction("Login");
        }

        // GET: /User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
                return View();
            }

            // Lưu session
            HttpContext.Session.SetInt32("UserID", user.Id);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
