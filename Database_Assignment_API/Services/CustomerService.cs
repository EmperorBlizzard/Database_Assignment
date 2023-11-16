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
    Task DeleteAsync(CustomerEntity customerEntity, AddressEntity addressEntity);
    Task<IEnumerable<CustomerModel>> GetAllAsync();
    Task<CustomerModel> GetOneAsync(CustomerModel customerModel);
    Task UpdateAsync(CustomerEntity customerEntity, AddressEntity addressEntity);
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
                addressEntity ??= await _addressRepo.CreatAsync(new AddressEntity{ StreetName = registration.StreetName, StreetNumber = registration.StreetNumber, PostalCode = registration.PostalCode, City = registration.City});

                var customerEntity = new CustomerEntity
                {
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    Email = registration.Email,
                    PhoneNumber = registration.PhoneNumber,
                    AddressId = addressEntity.Id,
                };

                customerEntity = await _customerRepo.CreatAsync(customerEntity);
                
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<IEnumerable<CustomerModel>> GetAllAsync()
    {
        try
        {
            var customers = await _customerRepo.GetAllAsync();

            return customers.Select(x => new CustomerModel()).ToList();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<CustomerModel> GetOneAsync(CustomerModel customerModel)
    {
        try
        {
            var entity = await _customerRepo.GetAsync(x => x.Id == customerModel.Id);

            var customer = new CustomerModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                StreetName = entity.Address.StreetName,
                StreetNumber = entity.Address.StreetNumber,
                PostalCode = entity.Address.PostalCode,
                City = entity.Address.City
            };

            return customer;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task UpdateAsync(CustomerEntity customerEntity, AddressEntity addressEntity)
    {
        try
        {
            await _addressRepo.UpdateAsync(addressEntity);
            await _customerRepo.UpdateAsync(customerEntity);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task DeleteAsync(CustomerEntity customerEntity, AddressEntity addressEntity)
    {
        try
        {
            await _customerRepo.DeleteAsync(customerEntity);
            await _addressRepo.DeleteAsync(addressEntity);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }
}
