using TerryTraining.Domain.Entities.OrderAggregate;

namespace TerryTraining.Domain.Interfaces;

public interface IOrderRepository
{
    public Task<Order> CreateOrder(Order order);
    public Task<Order> FulfillOrder(int orderId);
    public Task<Order> GetOrder(int orderId);
}