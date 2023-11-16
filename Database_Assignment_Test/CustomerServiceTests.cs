using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using Database_Assignment_API.Services;
using Moq;

namespace Database_Assignment_Test;

public class CustomerServiceTests
{

    [Fact]
    public async Task CreateCustomerAsync_Should_ReturnTrue_IfCreated()
    {
        //Arrange
        var addressEntity = new AddressEntity {Id = 1, StreetName = "Björkbacksvägen", StreetNumber = "37", PostalCode = "13540", City = "Tyresö" };
        
        ICustomerRegistration customerRegistration = new CustomerRegistration
        {
            FirstName = "Emil",
            LastName = "Olsson",
            Email = "emil111@live.se",
            PhoneNumber = "0735878761",
            StreetName = "Björkbacksvägen",
            StreetNumber = "37",
            PostalCode = "13540",
            City = "Tyresö"
        };

        var customerEntity = new CustomerEntity
        {
            Id = 1,
            FirstName = customerRegistration.FirstName,
            LastName = customerRegistration.LastName,
            Email = customerRegistration.Email,
            PhoneNumber = customerRegistration.PhoneNumber,
            AddressId = addressEntity.Id
        };


        var mockCustomerRepo = new Mock<ICustomerRepository>();
        var mockAddressRepo = new Mock<IAddressRepository>();


        mockCustomerRepo
            .Setup(repo => repo.ExistsAsync(x => x.Email == customerRegistration.Email))
            .ReturnsAsync(false);

        mockAddressRepo
            .Setup(service => service.GetAsync(x => x.StreetName == customerRegistration.StreetName && x.PostalCode == customerRegistration.PostalCode))
            .ReturnsAsync(addressEntity);

        mockCustomerRepo
            .Setup(repo => repo.CreateAsync(customerEntity))
            .ReturnsAsync(customerEntity);


        ICustomerService customerService = new CustomerService(mockCustomerRepo.Object, mockAddressRepo.Object);
        

        //Act
        var result = await customerService.CreateAsync(customerRegistration);


        //Assert
        Assert.True(result);


    }
}
