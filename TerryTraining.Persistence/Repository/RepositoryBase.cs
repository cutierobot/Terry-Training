using Microsoft.EntityFrameworkCore;
using TerryTraining.Domain.Interfaces;

namespace TerryTraining.Persistence.Repository;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly TerryDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public RepositoryBase(TerryDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}