using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TerryTraining.Application.DTO;
using TerryTraining.Application.Interfaces;
using TerryTraining.Persistence;
using TerryTraining.Persistence.Models;

namespace TerryTraining.Application.Services;

public class TerryTrainingService : ITerryTrainingService
{
    private readonly TerryDbContext _context;
    private readonly IValidator<Product> _productValidator;
    // private ITerryTrainingService _terryTrainingServiceImplementation;

    public TerryTrainingService(TerryDbContext context, IValidator<Product> productValidator)
    {
        _context = context;
        _productValidator = productValidator;
    }

    public async Task<ProductDTO> NewProduct(string name, string description, int stockcount)
    {
        try
        {
            var product = new Product
            {
                Description = description,
                Name = name,
                Stock = stockcount,
                Reserved = 0 // default of DB is 0 anyway, but still force sending it as best practice
            };

            ValidateProduct(product);

            //  Check doesn’t already exist
            var doesExist = await DoesProductExist(name, description);
            if (doesExist != 0)
            {
                throw new Exception("Product with that name already exists, ID: " + doesExist.ToString());
            }
            // This should probably all be done with FluentValidation????
            //  Check name, description sizes
            // done in ProductValidator

            //  Check stockCount > 0
            if (stockcount == 0)
            {
                throw new Exception("Product stockcount but be bigger then 0");
            }



            await _context.Product.AddAsync(product);
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

    public async Task<ProductDTO> GetProduct(int id)
    {
        var result = await _context.Product.FirstAsync(product => product.Id == id);
        return new ProductDTO
        {
            Id = result.Id,
            Description = result.Description,
            Name = result.Name,
            Stock = result.Stock,
            Reserved = result.Reserved
        };
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
        return await _context.Product
            .Where(product => product.Name == name && product.Description == description)
            .Select(product => product.Id) // get only ID column
            .FirstOrDefaultAsync(); // return ID, or 0 (as 0 is default for int)
    }


    // ----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Checks if a ProductDTO is valid for the db.
    /// </summary>
    /// <param name="product">The name of the product to check if it exists</param>
    /// <returns>boolean of if ProductDTO is valid or not</returns>
    public void ValidateProduct(Product product)
    {
        // return _productValidator.Validate(product).IsValid;
        var results = _productValidator.Validate(product);

        var errorsArray = new List<string>();

        if (!results.IsValid)
        {
            foreach (var failure in results.Errors)
            {
                var errorString = "Property " + failure.PropertyName + " failed validation. Error was: " +
                                  failure.ErrorMessage;
                Console.WriteLine(errorString);
                errorsArray.Add(errorString);
            }
        }

        if (errorsArray.Count > 0)
        {
            throw new Exception(string.Join("\n", errorsArray));
        }
    }
}