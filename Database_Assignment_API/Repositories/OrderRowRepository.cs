using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories
{
    public class OrderRowRepository : Repo<OrderRowEntity>
    {
        private readonly DataContext _context;
        public OrderRowRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
