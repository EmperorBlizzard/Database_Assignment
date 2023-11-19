using Database_Assignment_API.Contexts;
using Database_Assignment_API.Repositories;
using Database_Assignment_API.Services;
using Database_Assignment_ConsoleApp.Menus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database_Assignment_ConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Skola\Database\Assignment\Database_Assignment\Database_Assignment_API\Contexts\database_Assignment.mdf;Integrated Security=True"));

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInStockRepository, InStockRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderRowRepository, OrderRowRepository>();
            services.AddScoped<IPrimaryCategoryRepository, PrimaryCategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IInvoiceLineRepository, InvoiceLineRepository>();


            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IInvoiceService, InvoiceService>();


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