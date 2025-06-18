using TerryTraining.Application.Interfaces;
using TerryTraining.Domain.Interfaces;
using TerryTraining.Application.Interfaces;

namespace TerryTraining.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly TerryDbContext _context;
    public IProductRepository Products { get; private set; }

    public UnitOfWork(TerryDbContext context, IProductRepository productRepository)
    {
        _context = context;
        Products = productRepository;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}