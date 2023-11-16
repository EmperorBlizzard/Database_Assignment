using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Database_Assignment_API.Repositories
{
    public interface ISubCategoryRepository : IRepo<SubCategoryEntity>
    {
    }

    public class SubCategoryRepository : Repo<SubCategoryEntity>, ISubCategoryRepository
    {
        private readonly DataContext _context;
        public SubCategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<SubCategoryEntity>> GetAllAsync()
        {
            return await _context.SubCategories.Include(x => x.PrimaryCategory).ToListAsync();
        }

        public override async Task<SubCategoryEntity> GetAsync(Expression<Func<SubCategoryEntity, bool>> expression)
        {
            var result = await base.GetAsync(expression);
            var category = await _context.SubCategories.Include(x => x.PrimaryCategory).FirstOrDefaultAsync(x => x.Id == result.Id);
            return category!;
        }
    }
}
