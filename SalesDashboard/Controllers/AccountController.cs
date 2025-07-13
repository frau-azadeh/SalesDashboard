using Microsoft.AspNetCore.Mvc;

namespace SalesDashboard.Controllers
{
    public class AccountController : Controller
    {
        // اطلاعات کاربران به صورت ثابت (mock)
        private readonly Dictionary<string, (string Password, string Role)> users = new()
        {
            { "admin", ("admin1234", "Admin") },
            { "marketing", ("marketing1234", "Marketing") },
            { "sales", ("sales1234", "Sales") },
            { "account", ("account1234", "Accounting") },
            { "purches", ("purches1234", "Purchasing") },
        };

        // GET: نمایش فرم ورود
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: پردازش اطلاعات ورود
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (users.TryGetValue(username, out var userInfo) && userInfo.Password == password)
            {
                // ذخیره نام کاربری و نقش در سشن
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("Role", userInfo.Role);

                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Error = "نام کاربری یا رمز عبور اشتباه است.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
