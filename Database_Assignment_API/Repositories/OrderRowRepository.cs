using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

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
    }
}
