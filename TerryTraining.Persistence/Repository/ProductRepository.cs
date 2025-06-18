using Microsoft.EntityFrameworkCore;
using TerryTraining.Application.DTO;
using TerryTraining.Domain.Interfaces;
// using TerryTraining.Persistence.Models;
using TerryTraining.Domain.Entities;

namespace TerryTraining.Persistence.Repository;

public class ProductRepository: RepositoryBase<Product>, IProductRepository
{
    private readonly TerryDbContext _dbContext;
    public ProductRepository(TerryDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    // public async Task<IEnumerable<Product>> GetAllAsync()
    // {
    //     throw new NotImplementedException();
    //     // return await _dbContext.
    // }
    //
    // public Task<Product> GetByIdAsync(int id)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task AddAsync(Product entity)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task UpdateAsync(Product entity)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<Product> NewProduct(string name, string description, int stockcount)
    public async Task<Product> NewProduct(Product product)
    {
        // throw new NotImplementedException();
        // should I move the service stuff here
        await _dbContext.Product.AddAsync(product);
        // await _dbContext.SaveChangesAsync();
        return product;
    }
    
    // public async Task<User> GetUserWithOrdersAsync(int id)
    // {
    //     return await _context.Set<User>()
    //         .Include(u => u.Orders)
    //         .FirstOrDefaultAsync(u => u.Id == id);
    // }

    public async Task<Product> GetProduct(int id)
    {
        // throw new NotImplementedException();
        return await _dbContext.Product.FirstAsync(product => product.Id == id);
    }

    /// <summary>
    /// Checks if a Product already exists matching the name and description. If a match is ofund returns the Product ID,
    /// else returns 0
    /// </summary>
    /// <param name="name">The name of the product to check if it exists</param>
    /// <param name="description">The description of the product to check if it exists.</param>
    /// <returns>returns the Product id if it exists, 0 otherwise</returns>
    public async Task<int> DoesProductExist(string name, string description)
    {
        // throw new NotImplementedException();
        // Find the product by its name and return the ID or 0 if it doesn't exist
        return await _dbContext.Product
            .Where(product => product.Name == name && product.Description == description)
            .Select(product => product.Id) // get only ID column
            .FirstOrDefaultAsync(); // return ID, or 0 (as 0 is default for int)
    }
}