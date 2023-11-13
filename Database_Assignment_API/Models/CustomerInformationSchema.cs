using System.ComponentModel.DataAnnotations;

namespace Database_Assignment_API.Models;

public class CustomerInformationSchema
{
    [Required] public string InformationType { get; set; } = null!;
    [Required] public string InformationValue { get; set; } = null!;
}
