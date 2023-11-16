using Database_Assignment_API.Contexts;
using Database_Assignment_API.Entites;
using Database_Assignment_API.Models;
using Database_Assignment_API.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Database_Assignment_API.Services;

public interface IOrderService
{
    Task<bool> CreateAsync(OrderRegistration orderRegistration);
    Task<bool> DeleteAsync(OrderEntity orderEntity);
    Task<IEnumerable<OrderModel>> GetAllAsync();
    Task<OrderModel> GetOneAsync(Expression<Func<OrderEntity, bool>> predicate);
    Task<bool> UpdateAsync(OrderEntity orderEntity);
}

public class OrderService : IOrderService
{
    private readonly OrderRepository orderRepository;
    private readonly OrderRowRepository orderRowRepository;

    public OrderService(OrderRepository orderRepository, OrderRowRepository orderRowRepository)
    {
        this.orderRepository = orderRepository;
        this.orderRowRepository = orderRowRepository;
    }

    public async Task<bool> CreateAsync(OrderRegistration orderRegistration)
    {
        try
        {
            
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<IEnumerable<OrderModel>> GetAllAsync()
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<OrderModel> GetOneAsync(Expression<Func<OrderEntity, bool>> predicate)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    public async Task<bool> UpdateAsync(OrderEntity orderEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> DeleteAsync(OrderEntity orderEntity)
    {
        try
        {

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
}
