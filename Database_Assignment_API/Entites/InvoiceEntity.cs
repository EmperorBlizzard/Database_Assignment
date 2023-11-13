using System.ComponentModel.DataAnnotations.Schema;

namespace Database_Assignment_API.Entites;

public class InvoiceEntity
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime DueDate {  get; set; } = DateTime.Now.AddDays(30);

    public string CustomerNumber { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string? AddressLine {  get; set; } 
    public string? PostalCode { get; set; }
    public string? City { get; set; }

    [Column(TypeName = "Money")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal VAT {  get; set; }

    public ICollection<InvoiceLineEntity> InvoiceLines { get; set; } = new HashSet<InvoiceLineEntity>();
}
