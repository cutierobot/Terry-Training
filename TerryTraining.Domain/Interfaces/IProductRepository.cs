using TerryTraining.Domain.Entities;

namespace TerryTraining.Domain.Interfaces;

public interface IProductRepository: IRepositoryBase<Product>
{
    public Task<Product> NewProduct(string name, string description, int stockcount);
    public Task<Product> GetProduct(int id);
}