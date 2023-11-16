﻿namespace Database_Assignment_API.Models;

public class OrderModel
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(20);
    public decimal TotalPrice { get; set; }
    public decimal VAT { get; set; }


    public string CustomerEmail { get; set; } = null!;

    public string ProductArticleNumber { get; set; } = null!;

    public int Quantity { get; set; }
    public decimal Price { get; set; }
}