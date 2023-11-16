using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public class CategoryService
{
    private readonly SubCategoryRepository _subCategoryRepository;
    private readonly PrimaryCategoryRepository _primaryCategoryRepository;

    public CategoryService(SubCategoryRepository subCategoryRepository, PrimaryCategoryRepository primaryCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
        _primaryCategoryRepository = primaryCategoryRepository;
    }

    public async Task CreateAsync(SubCategoryService categoryEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    //public async Task<AddressModel> GetAllAsync()
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
    //}

    //public async Task<IEnumerable<PrimaryCategoryEntity>> GetOneAsync(Expression<Func<AddressEntity, bool>> predicate)
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
    //}

    public async Task UpdateAsync(SubCategoryService categoryEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task DeleteAsync(SubCategoryService categoryEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }
}
