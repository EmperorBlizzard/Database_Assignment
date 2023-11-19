using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Database_Assignment_API.Repositories
{
    public interface IOrderRepository : IRepo<OrderEntity>
    {
    }

    public class OrderRepository : Repo<OrderEntity>, IOrderRepository
    {
        private readonly DataContext _context;
        
        public OrderRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<OrderEntity>> GetAllAsync()
        {
            return await _context.Orders
                .Include(x => x.OrderRows)
                .Include(x => x.Customer)
                .ToListAsync();
        }

        public override async Task<OrderEntity> GetAsync(Expression<Func<OrderEntity, bool>> expression)
        {
            var result = await base.GetAsync(expression);
            var order = await _context.Orders
                .Include(x => x.OrderRows)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == result.Id);
            return order!;
        }
    }
}
