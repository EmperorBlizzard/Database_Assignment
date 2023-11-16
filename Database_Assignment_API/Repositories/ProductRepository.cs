using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Microsoft.EntityFrameworkCore;

namespace Database_Assignment_API.Repositories
{
    public interface IProductRepository : IRepo<ProductEntity>
    {
    }

    public class ProductRepository : Repo<ProductEntity>, IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            return await _context.Products
                .Include(x => x.Stock)
                .Include(x => x.SubCategory)
                .ThenInclude(x => x.PrimaryCategory)
                .ToListAsync();
        }
    }
}
