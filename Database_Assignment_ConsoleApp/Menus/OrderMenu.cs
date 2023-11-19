using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Services;

namespace Database_Assignment_ConsoleApp.Menus;

public class OrderMenu
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly IInvoiceService _invoiceService;
    private readonly ICustomerService _customerService;

    public OrderMenu(IOrderService orderService, IProductService productService, IInvoiceService invoiceService, ICustomerService customerService)
    {
        _orderService = orderService;
        _productService = productService;
        _invoiceService = invoiceService;
        _customerService = customerService;
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
            Console.WriteLine("3. View All Invoices");
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

                case "3":
                    await ViewAllInvoicesAsync();
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
        Console.WriteLine($"----Orders----");

        var orders = await _orderService.GetAllAsync();

        foreach (var order in orders)
        {
            Console.WriteLine();
            Console.WriteLine($"{order.OrderId} - {order.Order.TotalPrice:0.00}");
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

        var order = await _orderService.GetOneAsync(x => x.Id == orderId);

        if (order != null)
        {
            Console.WriteLine($"");
            Console.WriteLine($"Order:");
            Console.WriteLine($"");
            Console.WriteLine($"Order Id: {order.Id}");
            Console.WriteLine($"Order Date: {order.OrderDate}");
            Console.WriteLine($"Due Date: {order.DueDate}");
            Console.WriteLine($"Total Price: {order.TotalPrice:0.00} kr");
            Console.WriteLine($"VAT: {order.VAT:0.00} %");
            Console.WriteLine($"Customer Email: {order.Customer.Email}");
            Console.WriteLine("Products");

            foreach (var row in order.OrderRows)
            {
                Console.WriteLine("");
                Console.WriteLine($"Product Article Number: {row.ProductArticleNumber}");
                Console.WriteLine($"Price: {row.Price:0.00} kr.");
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

    public async Task DeleteOrderAsync(OrderEntity orderEntity)
    {
        Console.Write("Are you sure y/n: ");
        var option = Console.ReadLine()!.ToLower();

        if (option == "y")
        {
            var result = await _orderService.DeleteAsync(orderEntity);

            if (result == true)
            {
                Console.WriteLine("Order deleted");
            }
            else
            {
                Console.WriteLine("Something went wrong");
            }

            Console.ReadKey();
        }
    }

    public async Task ViewAllInvoicesAsync()
    {
        Console.Clear();

        var invoices = await _invoiceService.GetAllAsync();

        foreach (var invoice in invoices)
        {
            Console.WriteLine($"");
            Console.WriteLine($"{invoice.Invoice.Id} - {invoice.Invoice.CustomerNumber} - {invoice.Invoice.CustomerName}");
        }

        Console.WriteLine();
        Console.Write("Do you want to check details on a invoice y/n: ");
        var option = Console.ReadLine()!.ToLower();

        if (option == "y")
        {
            await ViewOneInvoiceAsync();
        }

    }

    public async Task ViewOneInvoiceAsync()
    {
        Console.Write("Invoice Id: ");
        var invoiceId = int.Parse(Console.ReadLine()!);

        var invoice = await _invoiceService.GetOneAsync(x => x.Id == invoiceId);

        if (invoice != null)
        {
            Console.WriteLine($"");
            Console.WriteLine($"Invoice:");
            Console.WriteLine($"Invoice Id: {invoice.Id}");
            Console.WriteLine($"Order Date: {invoice.OrderDate}");
            Console.WriteLine($"Invoice Due Date: {invoice.DueDate}");
            Console.WriteLine($"Customer Number: {invoice.CustomerNumber}");
            Console.WriteLine($"Customer Name: {invoice.CustomerName}");
            Console.WriteLine($"Address: {invoice.AddressLine}");
            Console.WriteLine($"Postal Code: {invoice.PostalCode}");
            Console.WriteLine($"City: {invoice.City}");
            Console.WriteLine($"Total price: {invoice.TotalAmount:0.00} kr");
            Console.WriteLine($"VAT: {invoice.VAT:0.00}");

            foreach (var line in invoice.InvoiceLines)
            {
                Console.WriteLine($"");
                Console.WriteLine($"Products: ");
                Console.WriteLine($"Article Number: {line.ProductArticleNumber}");
                Console.WriteLine($"Quantity: {line.Quantity} st");
                Console.WriteLine($"Price: {line.Price:0.00} kr");
            }
        }

        Console.WriteLine("Press any key to return to order manager");
        Console.ReadKey();

    }

    public async Task CreateOrderAsync()
    {
        Console.Clear();

        var orderReg = new OrderRegistration();

        var moreProducts = true;

        Console.WriteLine("----Create Order----");
        orderReg.OrderDate = DateTime.Now;
        orderReg.DueDate = DateTime.Now.AddDays(30);


        Console.Write("Customer Email: ");
        orderReg.CustomerEmail = Console.ReadLine()!;

        if (await _customerService.ExistsAsync(x => x.Email == orderReg.CustomerEmail))
        {
            Console.WriteLine("----Products----");
            var products = await _productService.GetAllAsync();

            foreach (var product in products)
            {
                Console.WriteLine();
                Console.WriteLine($"ArticlNumber: {product.ArticleNumber}");
                Console.WriteLine($"Product: {product.Name} - {product.SubCategory.SubCategoryName}");
                Console.WriteLine($"Price: {product.StockPrice:0.00}");
                Console.WriteLine($"Stock: {product.Stock.StockQuantity}");
            }

            do
            {

                var orderRowReg = new OrderRowRegistration();

                Console.WriteLine();
                Console.Write("Product ArticleNumber: ");

                orderRowReg.ProductArticleNumber = Console.ReadLine()!;

                Console.Write("Quantity: ");
                var quantity = Console.ReadLine()!;
                orderRowReg.Quantity = int.Parse(quantity);

                var product = await _productService.GetOneAsync(x => x.ArticleNumber == orderRowReg.ProductArticleNumber);

                orderRowReg.Price = product.StockPrice;

                var alredyInList = orderReg.Rows.FindIndex(x => x.ProductArticleNumber == orderRowReg.ProductArticleNumber);

                if (alredyInList >= 0)
                {
                    Console.WriteLine("Product Already Exist in List");
                    Console.WriteLine("Need to start over to change quantity!");
                    Console.ReadKey();
                }
                else
                {
                    if (orderRowReg.Quantity > product.Stock.StockQuantity)
                    {
                        Console.WriteLine("Not Enough in stock");
                    }
                    else
                    {
                        orderReg.Rows.Add(orderRowReg);

                        orderReg.TotalPrice += orderRowReg.Price * orderRowReg.Quantity;
                    }
                }

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

            decimal vat = 1.25m;
            var totalPrice = orderReg.TotalPrice;

            orderReg.TotalPrice = Decimal.Multiply(orderReg.TotalPrice, vat);

            orderReg.VAT = orderReg.TotalPrice - totalPrice;

            Console.WriteLine();

            Console.WriteLine("Your Order: ");
            Console.WriteLine($"Customer Email: {orderReg.CustomerEmail} ");
            Console.WriteLine($"Order Date: {orderReg.OrderDate}");
            Console.WriteLine($"Due Date: {orderReg.DueDate}");
            Console.WriteLine($"Total Price: {orderReg.TotalPrice:0.00}");
            Console.WriteLine($"VAT: {orderReg.VAT:0.00}");
            Console.WriteLine($"");
            Console.WriteLine($"Products:");


            foreach (var row in orderReg.Rows)
            {
                Console.WriteLine($"");
                Console.WriteLine($"Product Article Number: {row.ProductArticleNumber}");
                Console.WriteLine($"Quantity: {row.Quantity}");
                Console.WriteLine($"Price: {row.Price:0.00}");
            };

            Console.WriteLine("");
            Console.WriteLine("Does everything seem right?");
            Console.WriteLine("Ps. If somthing does not seem right you must start over!");

            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            if (choice == "y")
            {

                var result = await _orderService.CreateOrderAsync(orderReg);
                if (result == true)
                {
                    var invoiceResult = _invoiceService.CreateAsync(orderReg);

                    foreach (var row in orderReg.Rows)
                    {
                        var product = await _productService.GetOneAsync(x => x.ArticleNumber == row.ProductArticleNumber);

                        product.Stock.StockQuantity -= row.Quantity;

                        var stockUpdateResult = await _productService.UpdateStockAsync(product);

                    };
                    Console.WriteLine("Product created");
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Sending you back to Order manager");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Customer does not exist");
            Console.ReadKey();
        }
    }
}
