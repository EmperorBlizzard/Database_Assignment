using Database_Assignment_API.Services;
using Database_Assignment_ConsoleApp.Menus;
using Microsoft.Extensions.DependencyInjection;

namespace Database_Assignment_ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();


            services.AddScoped<MainMenu>();
            services.AddScoped<CustomerMenu>();
            services.AddScoped<OrderMenu>();
            services.AddScoped<ProductMenu>();
            services.AddScoped<CategoryMenu>();

            var sp = services.BuildServiceProvider();
            var mainMenu = sp.GetRequiredService<MainMenu>();

            await mainMenu.StartAsync();
        }
    }
}