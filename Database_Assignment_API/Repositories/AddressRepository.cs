using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories
{
    public interface IAddressRepository : IRepo<AddressEntity>
    {
    }

    public class AddressRepository : Repo<AddressEntity>, IAddressRepository
    {
        private readonly DataContext _context;
        public AddressRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
