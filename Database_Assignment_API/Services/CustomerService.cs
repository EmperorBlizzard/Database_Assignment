﻿using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public interface ICustomerService
{
    Task<bool> CreateAsync(ICustomerRegistration registration);
    Task<IEnumerable<CustomerModel>> GetAllAsync();
    Task<CustomerModel> GetOneAsync(Expression<Func<CustomerEntity, bool>> predicate);
    Task<bool> UpdateAsync(CustomerModel customerModel);
    Task<bool> DeleteAsync(CustomerModel customerModel);
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

    public async Task<CustomerModel> GetOneAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        try
        {
            var entity = await _customerRepo.GetAsync(predicate);
            
            if (entity != null)
            {
                var customer = new CustomerModel
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Email = entity.Email,
                    PhoneNumber = entity.PhoneNumber,
                    AddressId = entity.AddressId,
                    StreetName = entity.Address.StreetName,
                    StreetNumber = entity.Address.StreetNumber,
                    PostalCode = entity.Address.PostalCode,
                    City = entity.Address.City
                };

                return customer;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAsync(CustomerModel customerModel)
    {
        try
        {
            if( await _customerRepo.ExistsAsync(x => x.Email == customerModel.Email))
            {
                var customerEntity = new CustomerEntity
                {
                    Id = customerModel.Id,
                    FirstName = customerModel.FirstName,
                    LastName = customerModel.LastName,
                    Email = customerModel.Email,
                    PhoneNumber = customerModel.PhoneNumber
                };

                var addressEntity = new AddressEntity
                {
                    Id = customerModel.AddressId,
                    StreetName = customerModel.StreetName,
                    StreetNumber = customerModel.StreetNumber,
                    PostalCode = customerModel.PostalCode,
                    City = customerModel.City
                };


                await _addressRepo.UpdateAsync(addressEntity);
                await _customerRepo.UpdateAsync(customerEntity);
                return true;
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> DeleteAsync(CustomerModel customerModel)
    {
        try
        {
            var customerEntity = new CustomerEntity
            {
                Id = customerModel.Id,
                FirstName = customerModel.FirstName,
                LastName = customerModel.LastName,
                Email = customerModel.Email,
                PhoneNumber = customerModel.PhoneNumber
            };

            var addressEntity = new AddressEntity
            {
                Id = customerModel.AddressId,
                StreetName = customerModel.StreetName,
                StreetNumber = customerModel.StreetNumber,
                PostalCode = customerModel.PostalCode,
                City = customerModel.City
            };

            await _customerRepo.DeleteAsync(customerEntity);
            await _addressRepo.DeleteAsync(addressEntity);
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
