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
    Task<IEnumerable<ProductEntity>> GetAllAsync();
    Task<ProductEntity> GetOneAsync(Expression<Func<ProductEntity, bool>> predicate);
    Task<bool> UpdateAsync(ProductEntity productEntity);
    Task<bool> UpdateStockAsync(ProductEntity productEntity);
    Task<bool> DeleteAsync(ProductEntity productEntity, InStockEntity stockEntity);
    Task<bool> ExistsAsync(Expression<Func<ProductEntity, bool>> predicate);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IInStockRepository _inStockRepository;
    private readonly ICategoryService _categoryService;
    private readonly ISubCategoryService _subCategoryService;

    public ProductService(IProductRepository productRepository, IInStockRepository inStockRepository, ICategoryService categoryService, ISubCategoryService subCategoryService)
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
                
                var stockId = (await _inStockRepository.CreateAsync(new InStockEntity { StockQuantity = productRegistration.StockQuantity })).Id;
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

                entity = await _productRepository.CreateAsync(entity);

                return true;
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        try
        {
            var products = await _productRepository.GetAllAsync();
            

            return products;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<ProductEntity> GetOneAsync(Expression<Func<ProductEntity, bool>> predicate)
    {
        try
        {

            var customer = await _productRepository.GetAsync(predicate);


            return customer;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAsync(ProductEntity productEntity)
    {
        try
        {
            await _productRepository.UpdateAsync(productEntity);

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> UpdateStockAsync(ProductEntity productEntity)
    {
        try
        {
            await _inStockRepository.UpdateAsync(productEntity.Stock);

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> DeleteAsync(ProductEntity productEntity, InStockEntity stockEntity)
    {
        try
        {
            await _productRepository.DeleteAsync(productEntity);
            await _inStockRepository.DeleteAsync(stockEntity);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> ExistsAsync(Expression<Func<ProductEntity, bool>> predicate)
    {
        try
        {
            if(await _productRepository.ExistsAsync(predicate))
            {
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

}
