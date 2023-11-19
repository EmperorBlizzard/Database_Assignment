using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Database_Assignment_API.Repositories
{
    public interface IOrderRowRepository : IRepo<OrderRowEntity>
    {
    }

    public class OrderRowRepository : Repo<OrderRowEntity>, IOrderRowRepository
    {
        private readonly DataContext _context;
        public OrderRowRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<OrderRowEntity>> GetAllAsync()
        {
            return await _context.OrderRows
                .Include(x => x.Order)
                .ThenInclude(x => x.Customer)
                .ToListAsync();
        }




        public override async Task<OrderRowEntity> GetAsync(Expression<Func<OrderRowEntity, bool>> expression)
        {
            return await base.GetAsync(expression);
        }
    }
}
