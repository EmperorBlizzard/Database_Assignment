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
    Task<IEnumerable<OrderModel>> GetAllAsync();
    Task<OrderModel> GetOneAsync(Expression<Func<OrderEntity, bool>> predicate);
    Task<bool> DeleteAsync(OrderModel orderModel);
}

public class OrderService : IOrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly OrderRowRepository _orderRowRepository;
    private readonly CustomerService _customerService;
    private readonly ProductService _productService;

    public OrderService(OrderRepository orderRepository, OrderRowRepository orderRowRepository, CustomerService customerService, ProductService productService)
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

            foreach(var orderRow in orderRegistration.Rows)
            {
                if(!await _orderRowRepository.ExistsAsync(x => x.ProductArticleNumber == orderRow.ProductArticleNumber && x.OrderId == orderEntity.Id))
                {
                    var orderRowEntity = await CreateOrderRowAsync(orderEntity, orderRow);
                }
                else
                {
                    return false;
                }
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

    public async Task<IEnumerable<OrderModel>> GetAllAsync()
    {
        try
        {
            var order = await _orderRowRepository.GetAllAsync();

            return order.Select(x => new OrderModel()).ToList();
        }

        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<OrderModel> GetOneAsync(Expression<Func<OrderEntity, bool>> predicate)
    {
        try
        {
            var entity = await _orderRepository.GetAsync(predicate);

            var orderRows = await _orderRowRepository.GetAllAsync();

            orderRows.Select(x => x.OrderId == entity.Id).ToList();

            if (entity != null)
            {
                var order = new OrderModel
                {
                    OrderDate = entity.OrderDate,
                    DueDate = entity.DueDate,
                    TotalPrice = entity.TotalPrice,
                    VAT = entity.VAT,

                    CustomerEmail = entity.Customer.Email,
                };

                foreach (var row in orderRows)
                {
                    var orderRowModel = new OrderRowModel
                    {
                        ProductArticleNumber = row.ProductArticleNumber,
                        Quantity = row.Quantity,
                        Price = row.Price,
                    };

                    order.Rows.Add(orderRowModel);
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }
    public async Task<OrderRowEntity> GetOneRowAsync(Expression<Func<OrderRowEntity, bool>> predicate)
    {
        try
        {
            var orderRowEntity = await _orderRowRepository.GetAsync(predicate);
            return orderRowEntity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> DeleteAsync(OrderModel orderModel)
    {
        try
        {
            foreach (var rows in orderModel.Rows)
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

            var orderEntity = new OrderEntity
            {
                Id = orderModel.Id,
                OrderDate = orderModel.OrderDate,
                DueDate = orderModel.DueDate,
                TotalPrice = orderModel.TotalPrice,
                VAT = orderModel.VAT,
                CustomerId = (_customerService.GetOneAsync(x => x.Email == orderModel.CustomerEmail)).Id,
            };

            await _orderRepository.DeleteAsync(orderEntity);

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
}
