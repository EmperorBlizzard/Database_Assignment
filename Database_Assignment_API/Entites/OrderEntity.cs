using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database_Assignment_API.Entites;

public class OrderEntity
{
    public int Id { get; set; } 
    public DateTime OrderDate { get; set; }
    public DateTime DueDate { get; set; }
    [Column(TypeName = "Money")]
    public decimal TotalPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal VAT {  get; set; }

    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

    public ICollection<OrderRowEntity> OrderRows { get; set; } = new HashSet<OrderRowEntity>();

}
