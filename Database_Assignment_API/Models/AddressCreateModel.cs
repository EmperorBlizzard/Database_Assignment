namespace Database_Assignment_API.Models
{
    public class AddressCreateModel
    {
        public string StreetName { get; set; } = null!;
        public string? StreetNumber { get; set; }
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
