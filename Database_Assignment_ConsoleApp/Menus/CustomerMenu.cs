using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Services;

namespace Database_Assignment_ConsoleApp.Menus;

internal class CustomerMenu
{
    private readonly CustomerService _customerService;

    public CustomerMenu(CustomerService customerService)
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

    public async Task ViewOneCustomerAsync()
    {
        Console.Write("Customers email: ");
        var customerEmail = Console.ReadLine();

        CustomerModel customer = await _customerService.GetOneAsync(x => x.Email ==  customerEmail);

        if (customer != null)
        {
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone number: {customer.PhoneNumber}");
            Console.WriteLine($"Address: {customer.StreetName} {customer.StreetNumber} - {customer.City}");
            Console.WriteLine($"Postal Code: {customer.PostalCode}");
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

    public async Task EditCustomerAsync(CustomerModel customer)
    {
        Console.WriteLine("----Edit----");

        Console.WriteLine("First Name: ");
        customer.FirstName = Console.ReadLine()!;

        Console.WriteLine("Last Name: ");
        customer.LastName = Console.ReadLine()!;

        Console.WriteLine("Email: ");
        customer.Email = Console.ReadLine()!;

        Console.WriteLine("Phone Number: ");
        customer.PhoneNumber = Console.ReadLine()!;

        Console.WriteLine("Street Name: ");
        customer.StreetName = Console.ReadLine()!;

        Console.WriteLine("Street Number: ");
        customer.StreetNumber = Console.ReadLine()!;

        Console.WriteLine("Postal Code: ");
        customer.PostalCode = Console.ReadLine()!;

        Console.WriteLine("City: ");
        customer.City = Console.ReadLine()!;

        var result = await _customerService.UpdateAsync(customer);

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

    public async Task DeleteCustomerAsync(CustomerModel customer)
    {
        Console.Write("Are you sure y/n: ");
        var option = Console.ReadLine()!.ToLower();

        if (option == "y")
        {
            var result = await _customerService.DeleteAsync(customer);

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

        Console.WriteLine("First Name: ");
        customerReg.FirstName = Console.ReadLine()!;

        Console.WriteLine("Last Name: ");
        customerReg.LastName = Console.ReadLine()!;

        Console.WriteLine("Email: ");
        customerReg.Email = Console.ReadLine()!;

        Console.WriteLine("Phone Number: ");
        customerReg.PhoneNumber = Console.ReadLine()!;

        Console.WriteLine("Street Name: ");
        customerReg.StreetName = Console.ReadLine()!;

        Console.WriteLine("Street Number: ");
        customerReg.StreetNumber = Console.ReadLine()!;

        Console.WriteLine("Postal Code: ");
        customerReg.PostalCode = Console.ReadLine()!;

        Console.WriteLine("City: ");
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
