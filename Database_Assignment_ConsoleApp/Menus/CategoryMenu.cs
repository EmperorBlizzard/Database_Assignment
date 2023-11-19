using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Services;

namespace Database_Assignment_ConsoleApp.Menus;

public class CategoryMenu
{
    private readonly ISubCategoryService _subCategoryService;
    private readonly ICategoryService _categoryService;

    public CategoryMenu(ISubCategoryService subCategoryService, ICategoryService categoryService)
    {
        _subCategoryService = subCategoryService;
        _categoryService = categoryService;
    }

    public async Task CategoryManagerAsync()
    {
        var onOff = true;

        do
        {
            Console.Clear();

            Console.WriteLine("----Manage Categories----");
            Console.WriteLine("1. View All Categories");
            Console.WriteLine("2. Create Primary Category");
            Console.WriteLine("3. Create SubCategory");
            Console.WriteLine("0. Back");
            Console.WriteLine();
            Console.Write("Choose your option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ViewAllCategoriesAsync();
                    break;

                case "2":
                    await CreatePrimaryCategoryAsync();
                    break;

                case "3":
                    await CreateSubCategoryAsync();
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


    public async Task ViewAllCategoriesAsync()
    {
        Console.Clear();
        Console.WriteLine("----Categories----");

        var categories = await _subCategoryService.GetAllAsync();

        foreach (var category in categories)
        {
            Console.WriteLine($"Primary category: {category.PrimaryCategory.CategoryName} - Subcategory: {category.SubCategoryName}");
        }

        Console.WriteLine();
        Console.WriteLine("1. Edit PrimaryCategory");
        Console.WriteLine("2. Edit SubCategory");
        Console.WriteLine("0. Go Back");
        Console.Write("Choice: ");
        var option = Console.ReadLine()!.ToLower();

        switch (option)
        {
            case "1":
                await ViewOnePrimaryCategoryAsync();
                break;

            case "2":
                await ViewOneSubCategoryAsync();
                break;

            case "0":
                break;

            default:
                Console.WriteLine("Invalid option!");
                break;
        }
    }
    public async Task ViewOnePrimaryCategoryAsync()
    {
        Console.Write("Primary Category Name: ");
        var primaryCategoryName = Console.ReadLine();

        var categoryEntity = await _categoryService.GetOneAsync(x => x.CategoryName == primaryCategoryName);

        if (categoryEntity != null)
        {
            Console.WriteLine($"Category: {categoryEntity.CategoryName}");
            Console.WriteLine($"");

            Console.WriteLine("1. Edit Category");
            Console.WriteLine("2. Delete Category");
            Console.WriteLine("0. Go back");

            Console.Write("Choice: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await EditPrimaryCategoryAsync(categoryEntity);
                    break;

                case "2":
                    await DeletePrimaryCategoryAsync(categoryEntity);
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }

    public async Task EditPrimaryCategoryAsync(PrimaryCategoryEntity primaryCategoryEntity)
    {
        Console.WriteLine("----Edit----");

        Console.Write(" Primary Category Name: ");
        primaryCategoryEntity.CategoryName = Console.ReadLine()!;

        var result = await _categoryService.UpdateAsync(primaryCategoryEntity);

        if (result == true)
        {
            Console.WriteLine("Category Updated");
        }
        else
        {
            Console.WriteLine("Something went wrong / Email already exist");
        }

        Console.ReadKey();
    }

    public async Task DeletePrimaryCategoryAsync(PrimaryCategoryEntity primaryCategoryEntity)
    {
        Console.Write("Are you sure y/n: ");
        var option = Console.ReadLine()!.ToLower();

        if (option == "y")
        {
            var result = await _categoryService.DeleteAsync(primaryCategoryEntity);

            if (result == true)
            {
                Console.WriteLine("Category delete");
            }
            else
            {
                Console.WriteLine("Something went wrong / Has subcategories");
            }
        }

        Console.ReadKey();
    }


    public async Task ViewOneSubCategoryAsync()
    {
        Console.Write("Sub Category Name: ");
        var subCategoryName = Console.ReadLine();

        var subCategoryEntity = await _subCategoryService.GetOneAsync(x => x.SubCategoryName == subCategoryName);

        if (subCategoryEntity != null)
        {
            Console.WriteLine($"Category: {subCategoryEntity.PrimaryCategory.CategoryName} - {subCategoryEntity.SubCategoryName}");
            Console.WriteLine($"");

            Console.WriteLine("1. Edit Subcategory");
            Console.WriteLine("2. Delete Subcategory");
            Console.WriteLine("0. Go back");

            Console.Write("Choice: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await EditSubCategoryAsync(subCategoryEntity);
                    break;

                case "2":
                    await DeleteSubCategoryAsync(subCategoryEntity);
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }
    public async Task EditSubCategoryAsync(SubCategoryEntity subCategoryEntity)
    {
        Console.WriteLine("----Edit----");

        Console.Write(" SubCategory Name: ");
        subCategoryEntity.SubCategoryName = Console.ReadLine()!;

        Console.Write("Primary Category: ");
        subCategoryEntity.PrimaryCategory.CategoryName = Console.ReadLine()!;

        subCategoryEntity.PrimaryCategoryId = (await _categoryService.GetOneAsync(x => x.CategoryName == subCategoryEntity.PrimaryCategory.CategoryName)).Id;

        var result = await _subCategoryService.UpdateAsync(subCategoryEntity);

        if (result == true)
        {
            Console.WriteLine("Category Updated");
        }
        else
        {
            Console.WriteLine("Something went wrong");
        }

        Console.ReadKey();
    }

    public async Task DeleteSubCategoryAsync(SubCategoryEntity subCategoryEntity)
    {
        Console.Write("Are you sure y/n: ");
        var option = Console.ReadLine()!.ToLower();

        if (option == "y")
        {
            var result = await _subCategoryService.DeleteAsync(subCategoryEntity);

            if (result == true)
            {
                Console.WriteLine("Category delete");
            }
            else
            {
                Console.WriteLine("Something went wrong / Has subcategories");
            }
        }

        Console.ReadKey();
    }

    public async Task CreatePrimaryCategoryAsync()
    {
        Console.Clear();

        var categoryReg = new CategoryRegistration();

        Console.Write("Category Name: ");
        categoryReg.CategoryName = Console.ReadLine()!;

        var result = await _categoryService.CreateAsync(categoryReg);

        if (result == true)
        {
            Console.WriteLine("Category created");
        }
        else
        {
            Console.WriteLine("Something went wrong/Already exist");
        }

        Console.ReadKey();
    }

    public async Task CreateSubCategoryAsync()
    {
        Console.Clear();

        var subCategoryReg = new SubCategoryRegistration();

        Console.Write("Sub Category: ");
        subCategoryReg.SubCategoryName = Console.ReadLine()!;

        Console.Write("Primary Category: ");
        subCategoryReg.CategoryName = Console.ReadLine()!;

        var result = await _subCategoryService.CreateAsync(subCategoryReg);

        if (result == true)
        {
            Console.WriteLine("SubCategory created");
        }
        else
        {
            Console.WriteLine("Something went wrong/Already exist/Primary Category not exist");
        }

        Console.ReadKey();
    }
}
