using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Models;

public class CustomerCreateModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
