using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public class CustomerService
{
    private readonly DataContext _context;

    public CustomerService(DataContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CustomerEntity customerEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task<AddressModel> GetAllAsync()
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task<IEnumerable<AddressModel>> GetOneAsync(Expression<Func<AddressEntity, bool>> predicate)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task UpdateAsync(CustomerEntity customerEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task DeleteAsync(CustomerEntity customerEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }
}
