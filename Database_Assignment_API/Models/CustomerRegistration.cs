using System.ComponentModel.DataAnnotations;

namespace Database_Assignment_API.Models;

public interface ICustomerRegistration
{
    string City { get; set; }
    string Email { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string? PhoneNumber { get; set; }
    string PostalCode { get; set; }
    string StreetName { get; set; }
    string? StreetNumber { get; set; }
}
public class CustomerRegistration : ICustomerRegistration
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string StreetName { get; set; } = null!;
    public string? StreetNumber { get; set; }
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}
