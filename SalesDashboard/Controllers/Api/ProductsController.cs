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
            new Product { Id = 1, Name = "کیبورد", Price = 700000, Stock = 10 },
            new Product { Id = 2, Name = "موس", Price = 450000, Stock = 25 }
        };

        [HttpGet]
        public IActionResult GetAll() => Ok(products);

        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product updated)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            product.Name = updated.Name;
            product.Price = updated.Price;
            product.Stock = updated.Stock;
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            products.Remove(product);
            return NoContent();
        }
    }
}
