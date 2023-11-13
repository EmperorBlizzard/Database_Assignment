namespace Database_Assignment_API.Entites;

public class CustomerInformationTypeEntity
{
    public int Id { get; set; }
    public string InformationType { get; set; } = null!;

    public ICollection<CustomerInformationEntity> customerInformation { get; set; } = new HashSet<CustomerInformationEntity>();
}
