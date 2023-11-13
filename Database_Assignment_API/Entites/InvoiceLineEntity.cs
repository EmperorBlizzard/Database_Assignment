using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Database_Assignment_API.Entites;

public class InvoiceLineEntity
{
    [Key, Column(Order = 0)]
    public int InvoiceId { get; set; }
    public InvoiceEntity Invoice { get; set; } = null!;
    
    [Key, Column(Order = 1)]
    public string ProductArticleNumber { get; set; } = null!;

    public int Quantity { get; set; }

    [Column(TypeName = "Money")]
    public decimal Price { get; set; }
}
