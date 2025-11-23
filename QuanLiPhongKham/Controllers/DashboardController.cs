using Microsoft.AspNetCore.Mvc;

namespace QuanLiPhongKham.Controllers
{
    public class DashboardController : Controller
    {
        // GET: /Dashboard/
        public IActionResult Index()
        {
            return View();
        }
    }
}
