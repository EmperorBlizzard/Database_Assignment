using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public interface ISubCategoryService
{
    Task<bool> CreateAsync(SubCategoryRegistration subCategoryRegistration);
    Task<IEnumerable<SubCategoryModel>> GetAllAsync();
    Task<SubCategoryModel> GetOneAsync(Expression<Func<SubCategoryEntity, bool>> predicate);
    Task<bool> UpdateAsync(SubCategoryModel subCategoryModel);
    Task<bool> DeleteAsync(SubCategoryModel subCategoryModel);
}

public class SubCategoryService : ISubCategoryService
{
    private readonly SubCategoryRepository _subCategoryRepository;

    public SubCategoryService(SubCategoryRepository subCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
    }

    public async Task<bool> CreateAsync(SubCategoryRegistration subCategoryRegistration)
    {
        try
        {
            if (!await _subCategoryRepository.ExistsAsync(x => x.SubCategoryName == subCategoryRegistration.SubCategoryName))
            {
                var subCategoryEntity = new SubCategoryEntity
                {
                    SubCategoryName = subCategoryRegistration.SubCategoryName
                };

                subCategoryEntity = await _subCategoryRepository.CreateAsync(subCategoryEntity);

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<IEnumerable<SubCategoryModel>> GetAllAsync()
    {
        try
        {
            var categories = await _subCategoryRepository.GetAllAsync();
            return categories.Select(x => new SubCategoryModel()).ToList();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<SubCategoryModel> GetOneAsync(Expression<Func<SubCategoryEntity, bool>> predicate)
    {
        try
        {
            var categoryEntity = await _subCategoryRepository.GetAsync(predicate);

            var category = new SubCategoryModel
            {
                Id = categoryEntity.Id,
                SubCategoryName = categoryEntity.SubCategoryName,
                CategoryName = categoryEntity.PrimaryCategory.CategoryName
            };

            return category;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAsync(SubCategoryModel subCategoryModel)
    {
        try
        {
            var subCategoryEntity = new SubCategoryEntity
            {
                Id= subCategoryModel.Id,
                SubCategoryName= subCategoryModel.SubCategoryName,
                PrimaryCategoryId = subCategoryModel.PrimaryCategoryId,
            };

            await _subCategoryRepository.UpdateAsync(subCategoryEntity);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> DeleteAsync(SubCategoryModel subCategoryModel)
    {
        try
        {
            var subCategoryEntity = new SubCategoryEntity
            {
                Id = subCategoryModel.Id,
                SubCategoryName = subCategoryModel.SubCategoryName,
                PrimaryCategoryId = subCategoryModel.PrimaryCategoryId,
            };

            await _subCategoryRepository.DeleteAsync(subCategoryEntity);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
}
