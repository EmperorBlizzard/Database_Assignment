using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public interface IInvoiceService
{
    Task<bool> CreateAsync(OrderRegistration orderRegistration);
    Task<InvoiceLineEntity> CreateInvoiceLine(InvoiceEntity invoiceEntity, OrderRowRegistration orderRowRegistration);
    Task<IEnumerable<InvoiceLineEntity>> GetAllAsync();
    Task<InvoiceEntity> GetOneAsync(Expression<Func<InvoiceEntity, bool>> predicate);
    //Task<bool> DeleteAsync(InvoiceEntity invoiceEntity, InvoiceLineEntity invoiceLineEntity);
}
public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IInvoiceLineRepository _invoiceLineRepository;
    private readonly ICustomerService _customerService;

    public InvoiceService(IInvoiceRepository invoiceRepository, IInvoiceLineRepository invoiceLineRepository, ICustomerService customerService)
    {
        _invoiceRepository = invoiceRepository;
        _invoiceLineRepository = invoiceLineRepository;
        _customerService = customerService;
    }

    public async Task<bool> CreateAsync(OrderRegistration orderRegistration)
    {
        try
        {
            var customer = await _customerService.GetOneAsync(x => x.Email == orderRegistration.CustomerEmail);

            var invoiceEntity = new InvoiceEntity
            {
                OrderDate = orderRegistration.OrderDate,
                DueDate = orderRegistration.DueDate.AddDays(30),
                CustomerNumber = customer.Id.ToString(),
                CustomerName = $"{customer.FirstName} {customer.LastName}",
                AddressLine = $"{customer.Address.StreetName} {customer.Address.StreetNumber}",
                PostalCode = customer.Address.PostalCode,
                City = customer.Address.City,
                TotalAmount = orderRegistration.TotalPrice,
                VAT = orderRegistration.VAT,
            };

            invoiceEntity = await _invoiceRepository.CreateAsync(invoiceEntity);

            foreach (var row in orderRegistration.Rows)
            {
                var invoiceLine = await CreateInvoiceLine(invoiceEntity, row);
            }

            return true;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<InvoiceLineEntity> CreateInvoiceLine(InvoiceEntity invoiceEntity, OrderRowRegistration orderRowRegistration)
    {
        var invoiceLine = new InvoiceLineEntity
        {
            InvoiceId = invoiceEntity.Id,
            ProductArticleNumber = orderRowRegistration.ProductArticleNumber,
            Quantity = orderRowRegistration.Quantity,
            Price = orderRowRegistration.Price,
        };

        invoiceLine = await _invoiceLineRepository.CreateAsync(invoiceLine);

        return invoiceLine;
    }

    public async Task<IEnumerable<InvoiceLineEntity>> GetAllAsync()
    {
        try
        {
            var invoices = await _invoiceLineRepository.GetAllAsync();

            return invoices;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<InvoiceEntity> GetOneAsync(Expression<Func<InvoiceEntity, bool>> predicate)
    {
        try
        {
            var invoice = await _invoiceRepository.GetAsync(predicate);

            return invoice;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }
    
    //Not implemnted because it should only be removed after 7 - 10 years
    //public async Task<bool> DeleteAsync(InvoiceEntity invoiceEntity, InvoiceLineEntity invoiceLineEntity)
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex) { Debug.WriteLine(ex.Message); }

    //    return false;
    //}
}
