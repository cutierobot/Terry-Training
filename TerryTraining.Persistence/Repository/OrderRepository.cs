using TerryTraining.Domain.Entities.OrderAggregate;
using TerryTraining.Domain.Interfaces;

namespace TerryTraining.Persistence.Repository;

public class OrderRepository: RepositoryBase<Order>, IOrderRepository
{
    private readonly TerryDbContext _dbContext;
    public OrderRepository(TerryDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Order> CreateOrder(Order order)
    {
        // Products exist
        // Ensure customer exists
        // Ensure stock available
        // Reserve stock
        throw new NotImplementedException();
    }

    public Task<Order> FulfillOrder(int orderId)
    {
        // Ensure order exists
        // Update stock levels and reserve levels
        // Set order completed
        throw new NotImplementedException();
    }

    public Task<Order> GetOrder(int orderId)
    {
        // Ensure order exists
        // Return all details except customer details
        throw new NotImplementedException();
    }
}