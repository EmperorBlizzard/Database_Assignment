using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories
{
    public interface IInStockRepository : IRepo<InStockEntity>
    {
    }

    public class InStockRepository : Repo<InStockEntity>, IInStockRepository
    {
        private readonly DataContext _context;
        public InStockRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
