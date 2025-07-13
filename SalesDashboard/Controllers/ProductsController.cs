using Microsoft.AspNetCore.Mvc;

namespace SalesDashboard.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
