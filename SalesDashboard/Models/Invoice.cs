using System;
using System.Collections.Generic;

namespace SalesDashboard.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string CreatedBy { get; set; }
        public ICollection<InvoiceItem> Items { get; set; }
    }
}