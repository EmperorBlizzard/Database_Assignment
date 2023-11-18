using Database_Assignment_API.Models;
using Database_Assignment_API.Services;

namespace Database_Assignment_ConsoleApp.Menus;

internal class OrderMenu
{
    private readonly OrderService _orderService;
    private readonly ProductService _productService;

    public OrderMenu(OrderService orderService, ProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    public async Task ManageOrdersAsync()
    {
        var onOff = true;

        do
        {
            Console.Clear();

            Console.WriteLine("----Manage Orders----");
            Console.WriteLine("1. View all orders");
            Console.WriteLine("2. Create order");
            Console.WriteLine("0. Back");
            Console.WriteLine();
            Console.Write("Choose your option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ViewAllOrdersAsync();
                    break;

                case "2":
                    await CreateOrderAsync();
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

    public async Task ViewAllOrdersAsync()
    {
        Console.Clear();
        Console.WriteLine($"----Products----");

        var orders = await _orderService.GetAllAsync();

        foreach (var order in orders)
        {
            Console.WriteLine($"{order.Id} - {order.TotalPrice}");
        }

        Console.WriteLine();
        Console.Write("Do you want to check details on a order y/n: ");
        var option = Console.ReadLine()!.ToLower();

        if (option == "y")
        {
            await ViewOneOrderAsync();
        }
    }

    public async Task ViewOneOrderAsync()
    {
        Console.Write("Order Id: ");
        var orderId = int.Parse(Console.ReadLine()!);

        OrderModel order = await _orderService.GetOneAsync(x => x.Id == orderId);

        if (order != null)
        {
            Console.WriteLine($"Order Id: {order.Id}");
            Console.WriteLine($"Order Date: {order.OrderDate}");
            Console.WriteLine($"Due Date: {order.DueDate}");
            Console.WriteLine($"Total Price: {order.TotalPrice} kr");
            Console.WriteLine($"VAT: {order.VAT} %");
            Console.WriteLine($"Customer Email: {order.CustomerEmail}");
            Console.WriteLine("Products");

            foreach (var row in order.Rows)
            {
                Console.WriteLine("");
                Console.WriteLine($"Product Article Number: {row.ProductArticleNumber}");
                Console.WriteLine($"Price: {row.Price} kr.");
                Console.WriteLine($"Quantity: {row.Quantity} st.");

            }

            Console.WriteLine($"");

            Console.WriteLine("1. Delete Order");
            Console.WriteLine("0. Go back");

            Console.Write("Choice: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await DeleteOrderAsync(order);
                    break;

                case "0":
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }

    public async Task DeleteOrderAsync(OrderModel orderModel)
    {
        Console.Write("Are you sure y/n: ");
        var option = Console.ReadLine()!.ToLower();

        if (option == "y")
        {
            var result = await _orderService.DeleteAsync(orderModel);

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

    public async Task CreateOrderAsync()
    {
        Console.Clear();

        var orderReg = new OrderRegistration();
        var orderRowReg = new OrderRowRegistration();

        var moreProducts = true;

        Console.WriteLine("----Create Order----");

        Console.Write("Customer Email: ");
        orderReg.CustomerEmail = Console.ReadLine()!;

        Console.WriteLine("Products");
        await GetAllProductsForOrderAsync();

        

        do
        {
            Console.WriteLine();
            Console.Write("Product ArticleNumber ");

            orderRowReg.ProductArticleNumber = Console.ReadLine()!;

            Console.WriteLine("Quantity: ");
            orderRowReg.Quantity = int.Parse(Console.ReadLine()!);

            var product = await _productService.GetOneAsync(x => x.ArticleNumber == orderRowReg.ProductArticleNumber);

            orderRowReg.Price = product.StockPrice;

            orderReg.Rows.Add(orderRowReg);

            orderReg.TotalPrice += orderRowReg.Price * orderRowReg.Quantity;

            Console.WriteLine();

            Console.WriteLine("Do you want to add more products y/n");
            Console.Write("Chooice: ");
            var option = Console.ReadLine()!.ToLower();

            if (option == "n")
            {
                moreProducts = false;
            }
            else if (option == "y")
            {
            }
            else
            {
                Console.WriteLine("Invalid option");
                Console.WriteLine("Will not add any more products");
                moreProducts = false;

                Console.ReadKey();

            }

        } while (moreProducts);


        orderReg.VAT = 1.25m;

        orderReg.TotalPrice = Decimal.Multiply(orderReg.TotalPrice, orderReg.VAT);


        var result = await _orderService.CreateOrderAsync(orderReg);

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

    public async Task GetAllProductsForOrderAsync()
    {
        var products = await _productService.GetAllAsync();

        foreach (var product in products)
        {
            Console.WriteLine();
            Console.WriteLine($"{product.Name} - {product.CategoryName}");
            Console.WriteLine($"{product.StockPrice}");
        }
    }
}
