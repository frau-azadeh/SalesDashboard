using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesDashboard.Data;
using SalesDashboard.Models;

namespace SalesDashboard.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
                .AsNoTracking()
                .Select(p => new { p.Id, p.Name, p.Price, p.Stock })
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("اطلاعات محصول نامعتبر است.");

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updated)
        {
            if (updated == null || id != updated.Id)
                return BadRequest("شناسه ناهماهنگ است.");

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.Name = updated.Name;
            product.Price = updated.Price;
            product.Stock = updated.Stock;

            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
