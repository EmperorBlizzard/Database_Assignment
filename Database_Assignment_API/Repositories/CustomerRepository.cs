using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Microsoft.EntityFrameworkCore;

namespace Database_Assignment_API.Repositories
{
    public interface ICustomerRepository : IRepo<CustomerEntity>
    {
    }

    public class CustomerRepository : Repo<CustomerEntity>, ICustomerRepository
    {
        private readonly DataContext _context;
        public CustomerRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _context.Customers.Include(x => x.Address).ToListAsync();
        }
    }
}
