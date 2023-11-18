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
    Task<IEnumerable<ProductModel>> GetAllAsync();
    Task<ProductModel> GetOneAsync(Expression<Func<ProductEntity, bool>> predicate);
    Task<bool> UpdateAsync(ProductModel productModel);
    Task<bool> UpdateStockAsync(ProductModel productModel);
    Task<bool> DeleteAsync(ProductModel productModel);
    Task<bool> ExistsAsync(Expression<Func<ProductEntity, bool>> predicate);
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

    public async Task<IEnumerable<ProductModel>> GetAllAsync()
    {
        try
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(x => new ProductModel()).ToList();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<ProductModel> GetOneAsync(Expression<Func<ProductEntity, bool>> predicate)
    {
        try
        {

            var entity = await _productRepository.GetAsync(predicate);


            var customer = new ProductModel
            {
                ArticleNumber = entity.ArticleNumber,
                Name = entity.Name,
                Description = entity.Description,
                StockPrice = entity.StockPrice,
                StockQuantity = entity.Stock.StockQuantity,
                SubCategoryName = entity.SubCategory.SubCategoryName,
                CategoryName = entity.SubCategory.PrimaryCategory.CategoryName
            };

            return customer;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAsync(ProductModel productModel)
    {
        try
        {
            var productEntity = new ProductEntity
            {
                ArticleNumber = productModel.ArticleNumber,
                Name = productModel.Name,
                Description = productModel.Description,
                StockPrice = productModel.StockPrice,
            };

            await _productRepository.UpdateAsync(productEntity);

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> UpdateStockAsync(ProductModel productModel)
    {
        try
        {
            var stockEntity = new InStockEntity
            {
                Id = productModel.StockId,
                StockQuantity = productModel.StockQuantity,
            };

            await _inStockRepository.UpdateAsync(stockEntity);

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> DeleteAsync(ProductModel productModel)
    {
        try
        {
            var productEntity = new ProductEntity
            {
                ArticleNumber = productModel.ArticleNumber,
                Name = productModel.Name,
                Description = productModel.Description,
                StockPrice = productModel.StockPrice,
            };

            var stockEntity = new InStockEntity
            {
                Id = productModel.StockId,
                StockQuantity = productModel.StockQuantity
            };

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
