using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public override async Task<InvoiceEntity> GetAsync(Expression<Func<InvoiceEntity, bool>> expression)
    {
        var result = await base.GetAsync(expression);
        var invoice = await _context.Invoices.Include(x => x.InvoiceLines).FirstOrDefaultAsync();
        return invoice!;
    }
}
