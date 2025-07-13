using Microsoft.AspNetCore.Mvc;

namespace SalesDashboard.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
