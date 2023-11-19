using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public interface ICategoryService
{
    Task<bool> CreateAsync(CategoryRegistration categoryRegistration);
    Task<IEnumerable<PrimaryCategoryEntity>> GetAllAsync();
    Task<PrimaryCategoryEntity> GetOneAsync(Expression<Func<PrimaryCategoryEntity, bool>> predicate);
    Task<bool> UpdateAsync(PrimaryCategoryEntity categoryEntity);
    Task<bool> DeleteAsync(PrimaryCategoryEntity categoryEntity);
}

public class CategoryService : ICategoryService
{
    private readonly ISubCategoryService _subCategoryService;
    private readonly IPrimaryCategoryRepository _primaryCategoryRepository;

    public CategoryService(ISubCategoryService subCategoryService, IPrimaryCategoryRepository primaryCategoryRepository)
    {
        _subCategoryService = subCategoryService;
        _primaryCategoryRepository = primaryCategoryRepository;
    }

    public async Task<bool> CreateAsync(CategoryRegistration categoryRegistration)
    {
        try
        {
            if (!await _primaryCategoryRepository.ExistsAsync(x => x.CategoryName == categoryRegistration.CategoryName))
            {
                var categoryEntity = new PrimaryCategoryEntity
                {
                    CategoryName = categoryRegistration.CategoryName
                };

                categoryEntity = await _primaryCategoryRepository.CreateAsync(categoryEntity);

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<IEnumerable<PrimaryCategoryEntity>> GetAllAsync()
    {
        try
        {
            var categories = await _primaryCategoryRepository.GetAllAsync();
            

            return categories;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<PrimaryCategoryEntity> GetOneAsync(Expression<Func<PrimaryCategoryEntity, bool>> predicate)
    {
        try
        {
            var category = await _primaryCategoryRepository.GetAsync(predicate);

            return category;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAsync(PrimaryCategoryEntity categoryEntity)
    {
        try
        {
            await _primaryCategoryRepository.UpdateAsync(categoryEntity);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> DeleteAsync(PrimaryCategoryEntity categoryEntity)
    {
        try
        {
            var categories = await _subCategoryService.GetOneAsync(x => x.PrimaryCategoryId == categoryEntity.Id);

            if (categories == null)
            {
                await _primaryCategoryRepository.DeleteAsync(categoryEntity);

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
}
