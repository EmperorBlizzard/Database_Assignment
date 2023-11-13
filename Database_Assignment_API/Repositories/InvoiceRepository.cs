using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories;

public class InvoiceRepository : Repo<InvoiceEntity>
{
    private readonly DataContext _context;
    public InvoiceRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
