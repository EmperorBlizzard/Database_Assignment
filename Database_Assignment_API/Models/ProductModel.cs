namespace Database_Assignment_API.Models;

public class ProductModel
{
    public string ArticleNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal StockPrice { get; set; }

    public int StockQuantity { get; set; }

    public string CategoryName { get; set; } = null!;
    public string SubCategoryName { get; set; } = null!;
}
