using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SalesDashboard.Data;
using SalesDashboard.Models;

namespace SalesDashboard.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
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

            if (role != "Sales")
                return Unauthorized("شما اجازه ثبت فاکتور ندارید.");

            if (model == null || model.ProductIds == null || model.ProductIds.Count == 0)
                return BadRequest("اطلاعات فاکتور ناقص است.");

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
                    return NotFound($"محصول با شناسه {productId} پیدا نشد.");

                if (product.Stock < quantity)
                    return BadRequest($"موجودی '{product.Name}' کافی نیست.");

                product.Stock -= quantity;

                invoice.Items.Add(new InvoiceItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return Ok(new { message = "✅ فاکتور با موفقیت ثبت شد." });
        }
    }

    public class InvoiceCreateDto
    {
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
        public List<int> Quantities { get; set; }
    }
}
