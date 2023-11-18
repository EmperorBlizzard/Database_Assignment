namespace Database_Assignment_API.Models;

public class SubCategoryModel
{
    public int Id { get; set; }
    public string SubCategoryName { get; set; } = null!;

    public int PrimaryCategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
}
