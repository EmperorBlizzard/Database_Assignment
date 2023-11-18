namespace Database_Assignment_API.Models;

public class OrderRegistration
{
    public DateTime OrderDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal VAT { get; set; }


    public string CustomerEmail { get; set; } = null!;

    public List<OrderRowRegistration> Rows { get; set; } = new List<OrderRowRegistration>();
}
