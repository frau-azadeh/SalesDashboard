using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesDashboard.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Company { get; set; }
        public string? Note { get; set; }

        [JsonIgnore] // جلوگیری از نیاز به مقداردهی در ارسال/دریافت API
        public ICollection<Invoice>? Invoices { get; set; }
    }
}
