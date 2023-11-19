using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(OrderRegistration orderRegistration);
    Task<OrderRowEntity> CreateOrderRowAsync(OrderEntity orderEntity, OrderRowRegistration orderRowRegistration);
    Task<IEnumerable<OrderEntity>> GetAllAsync();
    Task<OrderEntity> GetOneAsync(Expression<Func<OrderEntity, bool>> predicate);
    Task<bool> DeleteAsync(OrderEntity orderEntity);
}

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderRowRepository _orderRowRepository;
    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;

    public OrderService(IOrderRepository orderRepository, IOrderRowRepository orderRowRepository, ICustomerService customerService, IProductService productService)
    {
        _orderRepository = orderRepository;
        _orderRowRepository = orderRowRepository;
        _customerService = customerService;
        _productService = productService;
    }

    public async Task<bool> CreateOrderAsync(OrderRegistration orderRegistration)
    {
        try
        {
            var customerId = (await _customerService.GetOneAsync(x => x.Email == orderRegistration.CustomerEmail)).Id;

            var orderEntity = new OrderEntity
            {
                OrderDate = orderRegistration.OrderDate,
                DueDate = orderRegistration.DueDate,
                TotalPrice = orderRegistration.TotalPrice,
                VAT = orderRegistration.VAT,
                CustomerId = customerId,
            };

            orderEntity = await _orderRepository.CreateAsync(orderEntity);

            foreach (var orderRow in orderRegistration.Rows)
            {
                var orderRowEntity = await CreateOrderRowAsync(orderEntity, orderRow);
            }

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<OrderRowEntity> CreateOrderRowAsync(OrderEntity orderEntity, OrderRowRegistration orderRowRegistration)
    {
        try
        {
            var orderRowEntity = new OrderRowEntity
            {
                OrderId = orderEntity.Id,
                ProductArticleNumber = orderRowRegistration.ProductArticleNumber,
                Quantity = orderRowRegistration.Quantity,
                Price = orderRowRegistration.Price
            };

            await _orderRowRepository.CreateAsync(orderRowEntity);

            return orderRowEntity;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<IEnumerable<OrderEntity>> GetAllAsync()
    {
        try
        {
            var orders = await _orderRepository.GetAllAsync();


            return orders;
        }

        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<OrderEntity> GetOneAsync(Expression<Func<OrderEntity, bool>> predicate)
    {
        try
        {
            var order = await _orderRepository.GetAsync(predicate);

            return order;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    

    public async Task<bool> DeleteAsync(OrderEntity orderEntity)
    {
        try
        {
            foreach (var rows in orderEntity.OrderRows)
            {
                var orderRowEntity = new OrderRowEntity
                {
                    OrderId = rows.OrderId,
                    ProductArticleNumber = rows.ProductArticleNumber,
                    Quantity = rows.Quantity,
                    Price = rows.Price,
                };

                await _orderRowRepository.DeleteAsync(orderRowEntity);
            }

            await _orderRepository.DeleteAsync(orderEntity);

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> OrderRowExistsAsync(Expression<Func<OrderRowEntity,bool>> predicate)
    {
        var result = await _orderRowRepository.ExistsAsync(predicate);
        return result;
    }
}
