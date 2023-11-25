namespace Database_Assignment_API.Models;

public interface ISubCategoryRegistration
{
    string SubCategoryName { get; set; }
    string CategoryName { get; set; }
}

public class SubCategoryRegistration : ISubCategoryRegistration
{
    public string SubCategoryName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}
