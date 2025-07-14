using Microsoft.AspNetCore.Mvc;

namespace SalesDashboard.Controllers
{
    public class ProductsController : Controller
    {
        // GET: /Products
        public IActionResult Index()
        {
            return View(); // باز کردن فایل Views/Products/Index.cshtml
        }
    }
}
