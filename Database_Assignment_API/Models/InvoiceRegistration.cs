namespace Database_Assignment_API.Models;

public class InvoiceRegistration
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(30);

    public string CustomerNumber { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string? AddressLine { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }

    public decimal TotalAmount { get; set; }
    public decimal VAT { get; set; }
}
