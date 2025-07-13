// Controllers/Api/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using SalesDashboard.Models;

namespace SalesDashboard.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new()
        {
            new Product { Id = 1, Name = "کیبورد", Price = 700_000, Stock = 10 },
            new Product { Id = 2, Name = "موس", Price = 450_000, Stock = 25 }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
            return Ok(product);
        }
    }
}
