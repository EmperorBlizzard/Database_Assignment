using Database_Assignment_API.Contexts;
using Database_Assignment_API.Models;

namespace Database_Assignment_API.Services;

public class AddressService
{
    private readonly DataContext _context;

    public AddressService(DataContext context)
    {
        _context = context;
    }

    public void CreateAddressAsync(AddressCreateModel model)
    {
        
    }
}
