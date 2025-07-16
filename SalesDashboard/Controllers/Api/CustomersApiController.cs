using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesDashboard.Data;
using SalesDashboard.Models;

namespace SalesDashboard.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/customersapi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers.AsNoTracking().ToListAsync();
            return Ok(customers);
        }

        // GET: api/customersapi/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        // POST: api/customersapi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        // PUT: api/customersapi/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer updated)
        {
            if (id != updated.Id)
                return BadRequest("شناسه مشتری ناهماهنگ است.");

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            customer.FullName = updated.FullName;
            customer.Email = updated.Email;
            customer.Phone = updated.Phone;
            customer.Company = updated.Company;
            customer.Note = updated.Note;

            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        // DELETE: api/customersapi/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
