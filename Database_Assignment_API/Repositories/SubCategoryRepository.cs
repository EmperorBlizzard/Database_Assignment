using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories
{
    public class SubCategoryRepository : Repo<SubCategoryEntity>
    {
        private readonly DataContext _context;
        public SubCategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
