using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories
{
    public interface IPrimaryCategoryRepository : IRepo<PrimaryCategoryEntity>
    {
    }

    public class PrimaryCategoryRepository : Repo<PrimaryCategoryEntity>, IPrimaryCategoryRepository
    {
        private readonly DataContext _context;
        public PrimaryCategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
