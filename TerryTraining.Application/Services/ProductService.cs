using Common;
using FluentValidation;
using TerryTraining.Application.DTO;
using TerryTraining.Application.Interfaces;
using TerryTraining.Domain.Entities;

// using TerryTraining.Persistence.Interfaces;
// using TerryTraining.Persistence.Models;

namespace TerryTraining.Application.Services;

// service should only get DTO and send back DTO

public class ProductService : IProductService
{
    // need to implement IUnitOfWork here now.
    // private readonly TerryDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ProductDTO> _productValidator;
    // private IProductService _terryTrainingServiceImplementation;

    // public ProductService(TerryDbContext context, IValidator<Product> productValidator)
    public ProductService(IUnitOfWork unitOfWork, IValidator<ProductDTO> productValidator)
    {
        // _context = context;
        _unitOfWork = unitOfWork;
        _productValidator = productValidator;
    }

    // public async Task<ProductDTO> NewProduct(string name, string description, int stockcount)
    public async Task<Result<ProductDTO>> NewProduct(ProductDTO productDTO)
    {
        try
        {
            // Call UnitOfWork here, which has the ProductRepository in it.
            var product = new Product
            {
                Description = productDTO.Description,
                Name = productDTO.Name,
                Stock = productDTO.Stock,
                Reserved = productDTO.Reserved // default of DB is 0 anyway, but still force sending it as best practice
            };

            // Issue #10 here
            var result = ValidateProduct(productDTO);
            if (result.IsFailure)
            {
                return (Result<ProductDTO>)Result.Failure(result.Error);
            }
            
            //  Check doesn’t already exist
            var doesExist = await DoesProductExist(productDTO.Name, productDTO.Description);
            if (doesExist != 0)
            {
                return Result.Failure<ProductDTO>(new Error(ErrorCodes.DuplicateError,"Product with that name already exists"));
            }
            
            // await _context.Product.AddAsync(product);
            // await _context.SaveChangesAsync();
            
            await _unitOfWork.Products.NewProduct(product);
            await _unitOfWork.CompleteAsync();
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
        // var result = await _context.Product.FirstAsync(product => product.Id == id);
        // return new ProductDTO
        // {
        //     Id = result.Id,
        //     Description = result.Description,
        //     Name = result.Name,
        //     Stock = result.Stock,
        //     Reserved = result.Reserved
        // };

        var result = await _unitOfWork.Products.GetProduct(id);
        return new ProductDTO
        {
            Id = result.Id,
            Description = result.Description,
            Name = result.Name,
            Stock = result.Stock,
            Reserved = result.Reserved
        };
    }

    public Task<ProductDTO> AddStock(ProductDTO product)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Checks if a Product already exists matching the name and description. If a match is found returns the Product ID,
    /// else returns 0
    /// </summary>
    /// <param name="name">The name of the product to check if it exists</param>
    /// <param name="description">The description of the product to check if it exists.</param>
    /// <returns>returns the Product id if it exists, 0 otherwise</returns>
    private async Task<int> DoesProductExist(string name, string description)
    {
        // Find the product by its name and return the ID or 0 if it doesn't exist
        return await _unitOfWork.Products.DoesProductExist(name, description);
    }


    // ----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Checks if a ProductDTO is valid for the db.
    /// </summary>
    /// <param name="product">The name of the product to check if it exists</param>
    /// <returns>boolean of if ProductDTO is valid or not</returns>
    public Result ValidateProduct(ProductDTO product)
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

        // return  errorsArray.Count <= 0;


        if (errorsArray.Count > 0)
        {
            return Result.Failure(new Error(ErrorCodes.ValidationError, errorsArray.ToString()));
        }
        
        return Result.Success();
    }
}