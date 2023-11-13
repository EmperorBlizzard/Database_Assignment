using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database_Assignment_API.Entites;

public class ProductEntity
{
    [Key]
    public string ArticleNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    [Column(TypeName = "Money")]
    public decimal StockPrice { get; set; }
    public int StockId { get; set; }
    public InStockEntity Stock { get; set; } = null!;

    public int SubCategoryId { get; set; }
    public SubCategoryEntity SubCategory { get; set; } = null!;

    public ICollection<OrderRowEntity> OrderRows { get; set; } = new HashSet<OrderRowEntity>();
}
