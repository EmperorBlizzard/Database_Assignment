using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public class InStockService
{
    private readonly DataContext _context;

    public InStockService(DataContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(InStockEntity stockEntity)
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

    public async Task UpdateAsync(InStockEntity stockEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task DeleteAsync(InStockEntity stockEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }
}
