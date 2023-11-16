using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories;

public interface IInvoiceRepository : IRepo<InvoiceEntity>
{
}

public class InvoiceRepository : Repo<InvoiceEntity>, IInvoiceRepository
{
    private readonly DataContext _context;
    public InvoiceRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
