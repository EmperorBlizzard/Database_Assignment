using Database_Assignment_API.Models;
using Database_Assignment_API.Services;

namespace Database_Assignment_ConsoleApp.Menus;

internal class ProductMenu
{
    private readonly ProductService _productService;
    private readonly SubCategoryService _subCategoryService;
    private readonly CategoryService _categoryService;
    private readonly CategoryMenu _categoryMenu;

    public ProductMenu(ProductService productService, SubCategoryService subcategoryService, CategoryService categoryService, CategoryMenu categoryMenu)
    {
        _productService = productService;
        _subCategoryService = subcategoryService;
        _categoryService = categoryService;
        _categoryMenu = categoryMenu;
    }

    public async Task ManageProductsAsync()
    {
        var onOff = true;

        do
        {
            Console.Clear();

            Console.WriteLine("----Manage Products----");
            Console.WriteLine("1. View all products");
            Console.WriteLine("2. Create product");
            Console.WriteLine("3. Category Manager");
            Console.WriteLine("0. Back");
            Console.WriteLine();
            Console.Write("Choose your option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ViewAllProductsAsync();
                    break;

                case "2":
                    await CreateProductAsync();
                    break;

                case "3":
                    await _categoryMenu.CategoryManagerAsync();
                    break;

                case "0":
                    onOff = false;
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }


        } while (onOff);
    }

    public async Task ViewAllProductsAsync()
    {
        Console.Clear();
        Console.WriteLine($"----Products----");

        var products = await _productService.GetAllAsync();

        foreach (var product in products)
        {
            Console.WriteLine();
            Console.WriteLine($"{product.Name} - {product.CategoryName}");
            Console.WriteLine($"{product.StockPrice}");
        }

        Console.WriteLine();
        Console.Write("Do you want to check details on a product y/n: ");
        var option = Console.ReadLine()!.ToLower();

        if (option == "y")
        {
            await ViewOneProductAsync();
        }
    }

    public async Task ViewOneProductAsync()
    {
        Console.Write("Article Number: ");
        var articleNumber = Console.ReadLine();

        ProductModel product = await _productService.GetOneAsync(x => x.ArticleNumber == articleNumber);

        if (product != null)
        {
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Price: {product.StockPrice} kr");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine($"Stock: {product.StockQuantity} st.");
            Console.WriteLine($"Category: {product.CategoryName} - {product.SubCategoryName}");
            Console.WriteLine($"");

            Console.WriteLine("1. Edit Product");
            Console.WriteLine("2. Delete Product");
            Console.WriteLine("3. Add To Stock");
            Console.WriteLine("0. Go back");

            Console.Write("Choice: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await EditProductAsync(product);
                    break;

                case "2":
                    await DeleteProductAsync(product);
                    break;

                case "3":
                    await AddToStockAsync(product);
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }

    public async Task EditProductAsync(ProductModel productModel)
    {
        Console.WriteLine("----Edit----");

        Console.WriteLine("Name: ");
        productModel.Name = Console.ReadLine()!;

        Console.WriteLine("Description: ");
        productModel.Description = Console.ReadLine()!;

        Console.WriteLine("Price: ");
        productModel.StockPrice = decimal.Parse(Console.ReadLine()!);

        Console.WriteLine("Subcategory Name: ");
        productModel.SubCategoryName = Console.ReadLine()!;

        var result = await _productService.UpdateAsync(productModel);

        if (result == true)
        {
            Console.WriteLine("Customer Updated");
        }
        else
        {
            Console.WriteLine("Something went wrong / Email already exist");
        }

        Console.ReadKey();
    }

    public async Task DeleteProductAsync(ProductModel productModel)
    {
        Console.Write("Are you sure y/n: ");
        var option = Console.ReadLine()!.ToLower();

        if (option == "y")
        {
            var result = await _productService.DeleteAsync(productModel);

            if (result == true)
            {
                Console.WriteLine("Customer delete");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }

            Console.ReadKey();
        }
    }

    public async Task AddToStockAsync(ProductModel productModel)
    {
        Console.Write("How many should be added: ");
        productModel.StockQuantity += int.Parse(Console.ReadLine()!);

        var result = await _productService.UpdateStockAsync(productModel);

        if (result == true)
        {
            Console.WriteLine("Customer delete");
        }
        else
        {
            Console.WriteLine("Something went wrong");
        }

        Console.ReadKey();
    }

    public async Task CreateProductAsync()
    {
        Console.Clear();

        var productReg = new ProductRegistration();

        Console.WriteLine("----Create Product----");

        Console.WriteLine("Name: ");
        productReg.Name = Console.ReadLine()!;

        Console.WriteLine("Description: ");
        productReg.Description = Console.ReadLine()!;

        Console.WriteLine("Price: ");
        productReg.StockPrice = decimal.Parse(Console.ReadLine()!);

        Console.WriteLine("Stock: ");
        productReg.StockQuantity = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Subcategory Name: ");
        productReg.SubCategoryName = Console.ReadLine()!;



        var result = await _productService.CreateAsync(productReg);

        if (result == true)
        {
            Console.WriteLine("Product created");
        }
        else
        {
            Console.WriteLine("Something went wrong/Already exist");
        }

        Console.ReadKey();
    }

    
}
