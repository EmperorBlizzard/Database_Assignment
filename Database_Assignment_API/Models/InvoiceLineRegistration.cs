namespace Database_Assignment_API.Models
{
    public class InvoiceLineRegistration
    {
        public string ProductArticleNumber { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
