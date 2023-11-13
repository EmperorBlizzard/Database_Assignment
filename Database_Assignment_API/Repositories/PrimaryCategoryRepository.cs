using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories
{
    public class PrimaryCategoryRepository : Repo<PrimaryCategoryEntity>
    {
        private readonly DataContext _context;
        public PrimaryCategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
