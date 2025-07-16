using System;

namespace SalesDashboard.Models
{
    public class WarehouseTransfer
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public string PerformedBy { get; set; } 
    }
}
