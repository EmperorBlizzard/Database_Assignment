namespace Database_Assignment_ConsoleApp.Menus;

public class MainMenu
{
    private readonly CustomerMenu _customerMenu;
    private readonly ProductMenu _productMenu;
    private readonly OrderMenu _orderMenu;

    public MainMenu(CustomerMenu customerMenu, ProductMenu productMenu, OrderMenu orderMenu)
    {
        _customerMenu = customerMenu;
        _productMenu = productMenu;
        _orderMenu = orderMenu;
    }

    public async Task StartAsync()
    {
        var onOff = true;

        do
        {
            Console.Clear();

            Console.WriteLine("----Main Menu----");
            Console.WriteLine("1. Manage Customers");
            Console.WriteLine("2. Manage Products");
            Console.WriteLine("3. Manage Orders");
            Console.WriteLine("0. Turn off");
            Console.WriteLine();
            Console.Write("Choose your option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await _customerMenu.ManageCustomersAsync();
                    break;

                case "2":
                    await _productMenu.ManageProductsAsync();
                    break;

                case "3":
                    await _orderMenu.ManageOrdersAsync();
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
}
