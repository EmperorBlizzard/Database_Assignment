using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public interface IInvoiceService
{
    Task<bool> CreateAsync(InvoiceRegistration invoiceRegistration);
    Task<IEnumerable<InvoiceModel>> GetAllAsync();
    Task<InvoiceModel> GetOneAsync(Expression<Func<InvoiceEntity, bool>> predicate);
    Task<bool> UpdateAsync(InvoiceEntity invoiceEntity, InvoiceLineEntity invoiceLineEntity);
    Task<bool> DeleteAsync(InvoiceEntity invoiceEntity, InvoiceLineEntity invoiceLineEntity);
}
public class InvoiceService : IInvoiceService
{
    public async Task<bool> CreateAsync(InvoiceRegistration invoiceRegistration)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
    public async Task<IEnumerable<InvoiceModel>> GetAllAsync()
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<InvoiceModel> GetOneAsync(Expression<Func<InvoiceEntity, bool>> predicate)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAsync(InvoiceEntity invoiceEntity, InvoiceLineEntity invoiceLineEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> DeleteAsync(InvoiceEntity invoiceEntity, InvoiceLineEntity invoiceLineEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
}
