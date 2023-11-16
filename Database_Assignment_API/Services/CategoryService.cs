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
    Task<IEnumerable<CategoryModel>> GetAllAsync();
    Task<bool> UpdateAsync(PrimaryCategoryEntity categoryEntity);
    Task<bool> DeleteAsync(PrimaryCategoryEntity categoryEntity);
}

public class CategoryService : ICategoryService
{
    private readonly SubCategoryService _subCategoryService;
    private readonly PrimaryCategoryRepository _primaryCategoryRepository;

    public CategoryService(SubCategoryService subCategoryService, PrimaryCategoryRepository primaryCategoryRepository)
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

    public async Task<IEnumerable<CategoryModel>> GetAllAsync()
    {
        try
        {
            var categories = await _primaryCategoryRepository.GetAllAsync();
            return categories.Select(x => new CategoryModel()).ToList();
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
