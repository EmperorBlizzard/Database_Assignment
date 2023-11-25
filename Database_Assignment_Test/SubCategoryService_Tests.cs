using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using Database_Assignment_API.Services;
using Moq;

namespace Database_Assignment_Test;

public class SubCategoryService_Tests
{
    [Fact]
    public async Task CreateSubCategoryAsync_Should_ReturnTrue_IfCreated()
    {
        //Arange
        var primaryCategoryEntity = new PrimaryCategoryEntity { Id = 1, CategoryName = "Computers" };

        ISubCategoryRegistration subCategoryRegistration = new SubCategoryRegistration
        {
            SubCategoryName = "TEST",
            CategoryName = "Computers"
        };

        var subCategoryEntity = new SubCategoryEntity
        {
            Id = 1,
            SubCategoryName = subCategoryRegistration.SubCategoryName,
            PrimaryCategoryId = primaryCategoryEntity.Id,
        };

        var mockSubCategoryRepository = new Mock<ISubCategoryRepository>();
        var mockPrimaryCategoryRepository = new Mock<IPrimaryCategoryRepository>();

        mockSubCategoryRepository
            .Setup(repo => repo.ExistsAsync(x => x.SubCategoryName == subCategoryRegistration.SubCategoryName))
            .ReturnsAsync(false);

        mockPrimaryCategoryRepository
            .Setup(service => service.GetAsync(x => x.CategoryName == subCategoryRegistration.CategoryName))
            .ReturnsAsync(primaryCategoryEntity);

        mockSubCategoryRepository
            .Setup(repo => repo.CreateAsync(subCategoryEntity))
            .ReturnsAsync(subCategoryEntity);

        var subCategoryService = new SubCategoryService(mockSubCategoryRepository.Object, mockPrimaryCategoryRepository.Object);


        //Act
        var result = await subCategoryService.CreateAsync(subCategoryRegistration);


        //Assert
        Assert.True(result);
    }
}
