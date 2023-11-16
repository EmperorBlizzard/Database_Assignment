using Microsoft.EntityFrameworkCore;

namespace Database_Assignment_API.Entites;

[Index(nameof(Email), IsUnique = true)]
public class CustomerEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }

    public int AddressId { get; set; }
    public AddressEntity Address { get; set; } = null!;

    public ICollection<OrderEntity> Order { get; set; } = new HashSet<OrderEntity>();
}
