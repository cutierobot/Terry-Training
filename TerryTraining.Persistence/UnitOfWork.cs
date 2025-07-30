using TerryTraining.Application.Interfaces;
using TerryTraining.Domain.Interfaces;
using TerryTraining.Application.Interfaces;
using TerryTraining.Persistence.Repository;

namespace TerryTraining.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly TerryDbContext _context;
    public IProductRepository Products { get; private set; }

    // public UnitOfWork(TerryDbContext context, IProductRepository productRepository)
    public UnitOfWork(TerryDbContext context)
    {
        //NOte: was originally haveing 2 contexts at same time
        _context = context;
        // Products = productRepository;
        Products = new ProductRepository(_context);
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