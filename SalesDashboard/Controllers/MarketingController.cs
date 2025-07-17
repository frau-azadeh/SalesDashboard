// Controllers/MarketingController.cs
using Microsoft.AspNetCore.Mvc;

namespace SalesDashboard.Controllers
{
    public class MarketingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
