using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesDashboard.Data;
using SalesDashboard.Models;

namespace SalesDashboard.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly AppDbContext _context;

        public InvoiceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(List<int> productIds, List<int> quantities)
        {
            var username = HttpContext.Session.GetString("Username") ?? "Unknown";
            var invoice = new Invoice
            {
                CreatedBy = username,
                Date = DateTime.Now,
                CustomerId = 1, // موقتی، بعداً از فرم انتخاب مشتری
                Items = new List<InvoiceItem>()
            };

            for (int i = 0; i < productIds.Count; i++)
            {
                var product = _context.Products.Find(productIds[i]);
                if (product == null || product.Stock < quantities[i])
                    continue;

                var item = new InvoiceItem
                {
                    ProductId = product.Id,
                    Quantity = quantities[i],
                    UnitPrice = product.Price
                };

                product.Stock -= item.Quantity;
                invoice.Items.Add(item);
            }

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Index", "Products");
        }
    }
}
