using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories
{
    public class ProductRepository : Repo<ProductEntity>
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
