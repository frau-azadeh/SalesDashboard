using System.ComponentModel.DataAnnotations;

namespace SalesDashboard.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Company { get; set; }

        public string Note { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
