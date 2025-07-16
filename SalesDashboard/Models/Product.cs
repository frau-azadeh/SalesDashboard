using System.ComponentModel.DataAnnotations;

namespace SalesDashboard.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Price {  get; set; }
        public int Stock { get; set; }
    }
}