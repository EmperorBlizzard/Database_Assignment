namespace Database_Assignment_API.Entites;

public class CustomerEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public int AddressId { get; set; }
    public AddressEntity Address { get; set; } = null!;

    public ICollection<CustomerInformationEntity> CustomerInformation { get; set; } = new HashSet<CustomerInformationEntity>();

    public ICollection<OrderEntity> Order { get; set; } = new HashSet<OrderEntity>();
}
