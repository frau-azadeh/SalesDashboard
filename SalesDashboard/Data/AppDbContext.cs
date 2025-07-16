using Microsoft.EntityFrameworkCore;
using SalesDashboard.Models;

namespace SalesDashboard.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoicesItem { get; set; }
        public DbSet<WarehouseTransfer> WarehouseTransfer { get; set; }
        }
}