using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesDashboard.Data;
using SalesDashboard.Models;
using SalesDashboard.Models.Dto;

namespace SalesDashboard.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvoiceApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceCreateDto model)
        {
            var username = "testuser"; // یا از سشن/هویت واقعی بگیر

            if (model == null || model.ProductIds == null || model.Quantities == null ||
                model.ProductIds.Count != model.Quantities.Count || model.ProductIds.Count == 0)
            {
                return BadRequest("❌ اطلاعات ناقص یا نامعتبر است.");
            }

            // بررسی وجود مشتری
            var customer = await _context.Customers.FindAsync(model.CustomerId);
            if (customer == null)
                return NotFound($"❌ مشتری با شناسه {model.CustomerId} یافت نشد.");

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
                    return NotFound($"❌ محصول با شناسه {productId} یافت نشد.");

                if (product.Stock < quantity)
                    return BadRequest($"❌ موجودی '{product.Name}' کافی نیست.");

                // کاهش موجودی
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

            return Ok(new { message = "✅ فاکتور با موفقیت ثبت شد." });
        }
    }
}
