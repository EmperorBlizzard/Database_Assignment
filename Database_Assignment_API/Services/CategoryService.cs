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
    Task<bool> UpdateAsync(CategoryModel categoryModel);
    Task<bool> DeleteAsync(CategoryModel categoryModel);
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

    public async Task<CategoryModel> GetOneAsync(Expression<Func<PrimaryCategoryEntity, bool>> predicate)
    {
        try
        {
            var categoryEntity = await _primaryCategoryRepository.GetAsync(predicate);

            var category = new CategoryModel
            {
                Id = categoryEntity.Id,
                CategoryName = categoryEntity.CategoryName,
            };

            return category;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAsync(CategoryModel categoryModel)
    {
        try
        {
            var categoryEntity = new PrimaryCategoryEntity
            {
                Id = categoryModel.Id,
                CategoryName = categoryModel.CategoryName,
            };

            await _primaryCategoryRepository.UpdateAsync(categoryEntity);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> DeleteAsync(CategoryModel categoryModel)
    {
        try
        {
            var categories = await _subCategoryService.GetOneAsync(x => x.PrimaryCategoryId == categoryModel.Id);

            if (categories == null)
            {
                var categoryEntity = new PrimaryCategoryEntity
                {
                    Id = categoryModel.Id,
                    CategoryName = categoryModel.CategoryName,
                };

                await _primaryCategoryRepository.DeleteAsync(categoryEntity);

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
}
