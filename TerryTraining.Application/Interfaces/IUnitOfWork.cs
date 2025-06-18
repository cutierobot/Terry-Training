using TerryTraining.Domain.Interfaces;

namespace TerryTraining.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    Task<int> CompleteAsync();
}