namespace Database_Assignment_API.Entites;

public class PrimaryCategoryEntity
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;

    public ICollection<SubCategoryEntity> SubCategories { get; set; } = new HashSet<SubCategoryEntity>();
}
