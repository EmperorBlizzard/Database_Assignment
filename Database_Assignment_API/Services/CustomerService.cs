using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public interface ICustomerService
{
    Task<bool> CreateAsync(ICustomerRegistration registration);
    Task<IEnumerable<CustomerEntity>> GetAllAsync();
    Task<CustomerEntity> GetOneAsync(Expression<Func<CustomerEntity, bool>> predicate);
    Task<bool> UpdateAsync(CustomerEntity customerEntity);
    Task<bool> DeleteAsync(CustomerEntity customerEntity, AddressEntity addressEntity);
    Task<bool> ExistsAsync(Expression<Func<CustomerEntity, bool>> predicate);
}

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepo;
    private readonly IAddressRepository _addressRepo;

    public CustomerService(ICustomerRepository customerRepo, IAddressRepository addressRepo)
    {
        _customerRepo = customerRepo;
        _addressRepo = addressRepo;
    }

    public async Task<bool> CreateAsync(ICustomerRegistration registration)
    {
        try
        {
            if (!await _customerRepo.ExistsAsync(x => x.Email == registration.Email))
            {
                var addressEntity = (await _addressRepo.GetAsync(x => x.StreetName == registration.StreetName && x.PostalCode == registration.PostalCode));
                addressEntity ??= await _addressRepo.CreateAsync(new AddressEntity{ StreetName = registration.StreetName, StreetNumber = registration.StreetNumber, PostalCode = registration.PostalCode, City = registration.City});

                var customerEntity = new CustomerEntity
                {
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    Email = registration.Email,
                    PhoneNumber = registration.PhoneNumber,
                    AddressId = addressEntity.Id
                };

                customerEntity = await _customerRepo.CreateAsync(customerEntity);
                
                return true;

            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        try
        {
            var customers = await _customerRepo.GetAllAsync();

            return customers;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<CustomerEntity> GetOneAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        try
        {
            var entity = await _customerRepo.GetAsync(predicate);
            
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAsync(CustomerEntity customerEntity)
    {
        try
        {
            if(! await _customerRepo.ExistsAsync(x => x.Email == customerEntity.Email))
            {
               
                var result1 = await _customerRepo.UpdateAsync(customerEntity);

                if(result1 != null)
                {
                    return true;
                }
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> DeleteAsync(CustomerEntity customerEntity, AddressEntity addressEntity)
    {
        try
        {
            await _customerRepo.DeleteAsync(customerEntity);
            if(!await _customerRepo.ExistsAsync(x => x.AddressId == addressEntity.Id))
            {
                await _addressRepo.DeleteAsync(addressEntity);
            }

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<bool> ExistsAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        try
        {
            if (await _customerRepo.ExistsAsync(predicate))
            {
                return true;
            }
            
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
}
