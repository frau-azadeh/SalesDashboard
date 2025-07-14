using Microsoft.EntityFrameworkCore;

namespace SalesDashboard.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        // در آینده می‌تونی جدول‌های دیگه هم اینجا اضافه کنی
    }
}
