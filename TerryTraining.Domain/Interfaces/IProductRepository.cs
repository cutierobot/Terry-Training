using TerryTraining.Domain.Entities;

namespace TerryTraining.Domain.Interfaces;

public interface IProductRepository: IRepositoryBase<Product>
{
    // public Task<Product> NewProduct(string name, string description, int stockcount);
    public Task<Product> NewProduct(Product product);
    public Task<Product> GetProduct(int id);
    public Task<int> DoesProductExist(string name, string description);
}