namespace SalesDashboard.Models.Dto
{
    public class InvoiceCreateDto
    {
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
        public List<int> Quantities { get; set; }
    }
}
