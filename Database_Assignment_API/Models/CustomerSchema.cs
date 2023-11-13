using System.ComponentModel.DataAnnotations;

namespace Database_Assignment_API.Models;

public class CustomerSchema
{
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string LastName { get; set; } = null!;
    [Required] public string StreetName { get; set; } = null!;
    public string? StreetNumber { get; set; }
    [Required] public string PostalCode { get; set; } = null!;
    [Required] public string City { get; set; } = null!;

    [Required] public ICollection<CustomerInformationSchema> CustomerInformation { get; set; } = new HashSet<CustomerInformationSchema>();
}
