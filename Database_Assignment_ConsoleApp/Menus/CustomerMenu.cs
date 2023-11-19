using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Services;

namespace Database_Assignment_ConsoleApp.Menus;

public class CustomerMenu
{
    private readonly ICustomerService _customerService;

    public CustomerMenu(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task ManageCustomersAsync()
    {
        var onOff = true;

        do
        {
            Console.Clear();

            Console.WriteLine("----Manage Customers----");
            Console.WriteLine("1. View all customers");
            Console.WriteLine("2. Create customer");
            Console.WriteLine("0. Back");
            Console.WriteLine();
            Console.Write("Choose your option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ViewAllCustomersAsync();
                    break;

                case "2":
                    await CreateCustomerAsync();
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

    public async Task ViewAllCustomersAsync()
    {
        Console.Clear();
        Console.WriteLine($"----Customers----");

        var customers = await _customerService.GetAllAsync();

        if (customers != null)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName} - {customer.Email}");
            }

            Console.WriteLine();
            Console.Write("Do you want to check details on a customer y/n: ");
            var option = Console.ReadLine()!.ToLower();

            if (option == "y")
            {
                await ViewOneCustomerAsync();
            }
        }
        else
        {
            Console.WriteLine("No Customers In system");
            Console.ReadKey();
        }
    }

    public async Task ViewOneCustomerAsync()
    {
        Console.Write("Customers email: ");
        var customerEmail = Console.ReadLine();

        var customer = await _customerService.GetOneAsync(x => x.Email ==  customerEmail);

        if (customer != null)
        {
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone number: {customer.PhoneNumber}");
            Console.WriteLine($"Address: {customer.Address.StreetName} {customer.Address.StreetNumber} - {customer.Address.City}");
            Console.WriteLine($"Postal Code: {customer.Address.PostalCode}");
            Console.WriteLine($"");

            Console.WriteLine("1. Edit Customer");
            Console.WriteLine("2. Delete Customer");
            Console.WriteLine("0. Go back");

            Console.Write("Choice: ");
            var option = Console.ReadLine();

            switch(option)
            {
                case "1":
                    await EditCustomerAsync(customer);
                    break;

                case "2":
                    await DeleteCustomerAsync(customer);
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }

    public async Task EditCustomerAsync(CustomerEntity customer)
    {
        Console.WriteLine("----Edit----");

        Console.Write("First Name: ");
        customer.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        customer.LastName = Console.ReadLine()!;

        Console.Write("Email: ");
        customer.Email = Console.ReadLine()!;

        Console.Write("Phone Number: ");
        customer.PhoneNumber = Console.ReadLine()!;

        Console.Write("Street Name: ");
        customer.Address.StreetName = Console.ReadLine()!;

        Console.Write("Street Number: ");
        customer.Address.StreetNumber = Console.ReadLine()!;

        Console.Write("Postal Code: ");
        customer.Address.PostalCode = Console.ReadLine()!;

        Console.Write("City: ");
        customer.Address.City = Console.ReadLine()!;

        var result = await _customerService.UpdateAsync(customer);

        if (result == true)
        {
            Console.WriteLine("Customer Updated");
        }
        else
        {
            Console.WriteLine("Something went wrong");
        }

        Console.ReadKey();
    }

    public async Task DeleteCustomerAsync(CustomerEntity customer)
    {
        Console.Write("Are you sure y/n: ");
        var option = Console.ReadLine()!.ToLower();

        if (option == "y")
        {
            var addressEntity = new AddressEntity
            {
                Id = customer.Address.Id,
                StreetName = customer.Address.StreetName,
                StreetNumber = customer.Address.StreetNumber,
                PostalCode = customer.Address.PostalCode,
                City = customer.Address.City,
            };

            var result = await _customerService.DeleteAsync(customer,addressEntity);

            if (result == true)
            {
                Console.WriteLine("Customer delete");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }
        }

        Console.ReadKey();

    }

    public async Task CreateCustomerAsync()
    {
        Console.Clear();

        var customerReg = new CustomerRegistration();

        Console.WriteLine("----Create Customer----");

        Console.Write("First Name: ");
        customerReg.FirstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        customerReg.LastName = Console.ReadLine()!;

        Console.Write("Email: ");
        customerReg.Email = Console.ReadLine()!;

        Console.Write("Phone Number: ");
        customerReg.PhoneNumber = Console.ReadLine()!;

        Console.Write("Street Name: ");
        customerReg.StreetName = Console.ReadLine()!;

        Console.Write("Street Number: ");
        customerReg.StreetNumber = Console.ReadLine()!;

        Console.Write("Postal Code: ");
        customerReg.PostalCode = Console.ReadLine()!;

        Console.Write("City: ");
        customerReg.City = Console.ReadLine()!;

        var result = await _customerService.CreateAsync(customerReg);

        if (result == true)
        {
            Console.WriteLine("Customer created");
        }
        else
        {
            Console.WriteLine("Something went wrong/Already exist");
        }

        Console.ReadKey();
    }
}
