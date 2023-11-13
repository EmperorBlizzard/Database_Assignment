namespace Database_Assignment_API.Entites;

public class AddressEntity
{
    public int Id { get; set; }
    public string StreetName { get; set; } = null!;
    public string? StreetNumber { get; set; }
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public ICollection<CustomerEntity> Customers { get; set; } = new HashSet<CustomerEntity>();
}
