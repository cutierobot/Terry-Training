using Common;
using TerryTraining.Application.DTO;
using TerryTraining.Domain.Entities;
// using TerryTraining.Persistence.Models;

namespace TerryTraining.Application.Interfaces;

public interface IProductService
{
    // returning string is only temp for testing
    // string NewProduct(string name, string description, int stockCount);
    // Task<ProductDTO> NewProduct(string name, string description, int stockCount);
    Task<Result<ProductDTO>> NewProduct(ProductDTO product);
    // Task<bool> DoesProductExist(string name, string description);
    
    // add the other method definitions here
    
    Task<ProductDTO> GetProduct(int id);
    
    Task<ProductDTO> AddStock(ProductDTO product);
    // ----------------------------------------------------------------------------------------------------------------
    Result ValidateProduct(ProductDTO product); // FluentValidation
}