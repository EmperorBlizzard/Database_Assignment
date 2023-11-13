using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories
{
    public class CustomerInformationTypeRepository : Repo<CustomerInformationTypeEntity>
    {
        private readonly DataContext _context;
        public CustomerInformationTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
