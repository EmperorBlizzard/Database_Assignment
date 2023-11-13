﻿using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;

namespace Database_Assignment_API.Repositories
{
    public class CustomerRepository : Repo<CustomerEntity>
    {
        private readonly DataContext _context;
        public CustomerRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
