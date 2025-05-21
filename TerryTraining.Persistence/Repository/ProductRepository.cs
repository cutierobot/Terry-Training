using TerryTraining.Domain.Interfaces;
using TerryTraining.Persistence.Models;

namespace TerryTraining.Persistence.Repository;

public class ProductRepository: RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(TerryDbContext dbContext) : base(dbContext)
    {
    }

    public Task<IEnumerable<Domain.Entities.Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.Product> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Domain.Entities.Product entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.Entities.Product entity)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.Product> NewProduct(string name, string description, int stockcount)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.Product> GetProduct(int id)
    {
        throw new NotImplementedException();
    }
}