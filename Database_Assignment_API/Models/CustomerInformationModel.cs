namespace Database_Assignment_API.Models;

public class CustomerInformationModel
{
    public int CustomerId { get; set; }
    public int TypeId { get; set; }
    public string Value { get; set; } = null!;
}
