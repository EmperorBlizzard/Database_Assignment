using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public interface ISubCategoryService
{
    Task<bool> CreateAsync(ISubCategoryRegistration subCategoryRegistration);
    Task<IEnumerable<SubCategoryEntity>> GetAllAsync();
    Task<SubCategoryEntity> GetOneAsync(Expression<Func<SubCategoryEntity, bool>> predicate);
    Task<bool> UpdateAsync(SubCategoryEntity subCategoryEntity);
    Task<bool> DeleteAsync(SubCategoryEntity subCategoryEntity);
}

public class SubCategoryService : ISubCategoryService
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly IPrimaryCategoryRepository _primaryCategoryRepository;

    public SubCategoryService(ISubCategoryRepository subCategoryRepository, IPrimaryCategoryRepository primaryCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
        _primaryCategoryRepository = primaryCategoryRepository;
    }

    public async Task<bool> CreateAsync(ISubCategoryRegistration subCategoryRegistration)
    {
        try
        {
            if (!await _subCategoryRepository.ExistsAsync(x => x.SubCategoryName == subCategoryRegistration.SubCategoryName))
            {
                var primaryCategoryEntity = (await _primaryCategoryRepository.GetAsync(x => x.CategoryName == subCategoryRegistration.CategoryName));

                var subCategoryEntity = new SubCategoryEntity
                {
                    SubCategoryName = subCategoryRegistration.SubCategoryName,
                    PrimaryCategoryId = primaryCategoryEntity.Id
                };

                subCategoryEntity = await _subCategoryRepository.CreateAsync(subCategoryEntity);

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<IEnumerable<SubCategoryEntity>> GetAllAsync()
    {
        try
        {
            var categories = await _subCategoryRepository.GetAllAsync();

            return categories;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<SubCategoryEntity> GetOneAsync(Expression<Func<SubCategoryEntity, bool>> predicate)
    {
        try
        {
            var category = await _subCategoryRepository.GetAsync(predicate);


            return category;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAsync(SubCategoryEntity subCategoryEntity)
    {
        try
        {
            await _subCategoryRepository.UpdateAsync(subCategoryEntity);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> DeleteAsync(SubCategoryEntity subCategoryEntity)
    {
        try
        {
            await _subCategoryRepository.DeleteAsync(subCategoryEntity);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
}
