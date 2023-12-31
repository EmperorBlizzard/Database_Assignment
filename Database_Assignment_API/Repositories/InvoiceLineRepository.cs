﻿using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Microsoft.EntityFrameworkCore;

namespace Database_Assignment_API.Repositories;

public interface IInvoiceLineRepository : IRepo<InvoiceLineEntity>
{
}

public class InvoiceLineRepository : Repo<InvoiceLineEntity>, IInvoiceLineRepository
{
    private readonly DataContext _context;
    public InvoiceLineRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<InvoiceLineEntity>> GetAllAsync()
    {
        return await _context.InvoiceLines.Include(x => x.Invoice).ToListAsync();
    }
}
