using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SalesDashboard.Data;
using SalesDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDashboard.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvoiceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceCreateDto model)
        {
            var role = HttpContext.Session.GetString("Role");
            var username = HttpContext.Session.GetString("Username");

            // فقط کاربران نیروی فروش دسترسی دارند
            if (role != "Sales")
                return Unauthorized("You are not authorized to create invoices.");

            if (model == null || model.ProductIds == null || model.ProductIds.Count == 0)
                return BadRequest("Invalid invoice data.");

            // ساخت فاکتور
            var invoice = new Invoice
            {
                CustomerId = model.CustomerId,
                CreatedBy = username,
                Date = DateTime.Now,
                Items = new List<InvoiceItem>()
            };

            for (int i = 0; i < model.ProductIds.Count; i++)
            {
                var productId = model.ProductIds[i];
                var quantity = model.Quantities[i];

                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                    return NotFound($"Product with ID {productId} not found.");

                if (product.Stock < quantity)
                    return BadRequest($"Product '{product.Name}' does not have enough stock.");

                product.Stock -= quantity;

                invoice.Items.Add(new InvoiceItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Invoice created successfully." });
        }
    }

    public class InvoiceCreateDto
    {
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
        public List<int> Quantities { get; set; }
    }
}
