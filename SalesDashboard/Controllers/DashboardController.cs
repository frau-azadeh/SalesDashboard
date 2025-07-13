using Microsoft.AspNetCore.Mvc;

namespace SalesDashboard.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            // فقط کاربر وارد شده اجازه دیدن داشبورد دارد
            if (HttpContext.Session.GetString("Username") == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");

            return View();
        }
    }
}
