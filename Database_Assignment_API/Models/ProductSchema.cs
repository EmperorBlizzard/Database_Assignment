using Database_Assignment_API.Entites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database_Assignment_API.Models;

public class ProductSchema
{
    [Required] public string ArticleNumber { get; set; } = null!;
    [Required] public string Name { get; set; } = null!;
    [Required] public string? Description { get; set; }
    [Required] public decimal StockPrice { get; set; }
    [Required] public int StockQuantity { get; set; }
    [Required] public string CategoryName { get; set; } = null!;
    [Required] public string SubCategoryName { get; set; } = null!;
}
