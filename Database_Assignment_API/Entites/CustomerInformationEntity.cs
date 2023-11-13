using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database_Assignment_API.Entites;

public class CustomerInformationEntity
{
    [Key, Column(Order = 0)]
    public int CustomerId {  get; set; }
    public CustomerEntity Customer { get; set; } = null!;
    [Key, Column(Order = 1)]
    public int TypeId { get; set; }
    public CustomerInformationTypeEntity Type { get; set; } = null!;

    public string InformationValue { get; set; } = null!;
}
