using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

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
    }
}
