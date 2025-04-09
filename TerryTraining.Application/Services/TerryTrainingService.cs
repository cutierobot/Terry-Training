using Microsoft.EntityFrameworkCore;
using TerryTraining.Application.DTO;
using TerryTraining.Application.Interfaces;
using TerryTraining.Persistence;
using TerryTraining.Persistence.Models;

namespace TerryTraining.Application.Services;

public class TerryTrainingService: ITerryTrainingService
{
    private readonly TerryDbContext _context;
    private ITerryTrainingService _terryTrainingServiceImplementation;

    public TerryTrainingService(TerryDbContext context)
    {
        _context = context;
    }
    public async Task<ProductDTO> NewProduct(string name, string description, int stockcount)
    {
        try
        {
            //  Check doesn’t already exist
            var doesExist = await DoesProductExist(name, description);
            if (doesExist != 0)
            {
                throw new Exception("Product with that name already exists, ID: " + doesExist.ToString());
            }
            // This should probably all be done with FluentValidation????
            //  Check name, description sizes
            //  Check stockcount > 0

            var product = new Product
            {
                Description = description,
                Name = name,
                Stock = stockcount,
                Reserved = 0
            };
            
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            Console.WriteLine("Successfully added product");

            return new ProductDTO
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                Stock = product.Stock,
                Reserved = product.Reserved
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    
    /// <summary>
    /// Checks if a Product already exists matching the name and description. If a match is ofund returns the Product ID,
    /// else returns 0
    /// </summary>
    /// <param name="name">The name of the product to check if it exists</param>
    /// <param name="description">The description of the product to check if it exists.</param>
    /// <returns>returns the Product id if it exists, 0 otherwise</returns>
    private async Task<int> DoesProductExist(string name, string description)
    {
        // Find the product by its name and return the ID or 0 if it doesn't exist
        return await _context.Products
            .Where(product => product.Name == name && product.Description == description)
            .Select(product => product.Id) // get only ID column
            .FirstOrDefaultAsync(); // return ID, or 0 (as 0 is defualt for int)
    }
}