using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories;

public class InvoiceLineRepository : Repo<InvoiceLineEntity>
{
    private readonly DataContext _context;
    public InvoiceLineRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
