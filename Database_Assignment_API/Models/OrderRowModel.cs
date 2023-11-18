namespace Database_Assignment_API.Models;

public class OrderRowModel
{
    public int OrderId { get; set; }
    public string ProductArticleNumber { get; set; } = null!;

    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
