using Database_Assignment_API.Entites;
using Database_Assignment_API.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public class SubCategoryService
{
    private readonly SubCategoryRepository _subCategoryRepository;

    public SubCategoryService(SubCategoryRepository subCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
    }

    public async Task CreateAsync(SubCategoryEntity categoryEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    //public async Task<IEnumerable<SubCategoryEntity>> GetAllAsync()
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex) { Debug.WriteLine(ex.Message); }
    //}

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

    public async Task UpdateAsync(SubCategoryEntity categoryEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task DeleteAsync(SubCategoryEntity categoryEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }
}
