using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories
{
    public class CustomerInformationRepository : Repo<CustomerInformationEntity>
    {
        private readonly DataContext _context;
        public CustomerInformationRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
