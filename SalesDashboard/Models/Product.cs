// Models/Product.cs
namespace SalesDashboard.Models
{
    public class Product
    {
        public int Id { get; set; }  // Auto-increment
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; } // تعداد موجودی
    }
}
