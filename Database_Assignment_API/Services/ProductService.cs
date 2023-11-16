using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public interface IProductService
{
    Task<bool> CreateAsync(ProductRegistration productRegistration);
    Task DeleteAsync(ProductEntity productEntity);
    Task<ProductModel> GetAllAsync();
    Task<IEnumerable<ProductModel>> GetOneAsync(Expression<Func<AddressEntity, bool>> predicate);
    Task UpdateAsync(ProductEntity productEntity);
}

public class ProductService : IProductService
{
    private readonly ProductRepository _productRepository;
    private readonly InStockRepository _inStockRepository;
    private readonly CategoryService _categoryService;
    private readonly SubCategoryService _subCategoryService;

    public ProductService(ProductRepository productRepository, InStockRepository inStockRepository, CategoryService categoryService, SubCategoryService subCategoryService)
    {
        _productRepository = productRepository;
        _inStockRepository = inStockRepository;
        _categoryService = categoryService;
        _subCategoryService = subCategoryService;
    }

    public async Task<bool> CreateAsync(ProductRegistration productRegistration)
    {
        try
        {
            if (!await _productRepository.ExistsAsync(x => x.ArticleNumber == productRegistration.ArticleNumber))
            {
                var stockId = (await _inStockRepository.CreatAsync(new InStockEntity { StockQuantity = productRegistration.StockQuantity })).Id;
                var subCategoryId = (await _subCategoryService.GetOneAsync(x => x.SubCategoryName == productRegistration.SubCategoryName)).Id;

                var entity = new ProductEntity
                {
                    ArticleNumber = productRegistration.ArticleNumber,
                    Name = productRegistration.Name,
                    Description = productRegistration.Description,
                    StockPrice = productRegistration.StockPrice,
                    StockId = stockId,
                    SubCategoryId = subCategoryId
                };

                entity = await _productRepository.CreatAsync(entity);

                return true;
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<ProductModel> GetAllAsync()
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task<IEnumerable<ProductModel>> GetOneAsync(Expression<Func<AddressEntity, bool>> predicate)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task UpdateAsync(ProductEntity productEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task DeleteAsync(ProductEntity productEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }
}
