using TerryTraining.Application.DTO;
using TerryTraining.Domain.Entities;
// using TerryTraining.Persistence.Models;

namespace TerryTraining.Application.Interfaces;

public interface ITerryTrainingService
{
    // returning string is only temp for testing
    // string NewProduct(string name, string description, int stockCount);
    // Task<ProductDTO> NewProduct(string name, string description, int stockCount);
    Task<ProductDTO> NewProduct(ProductDTO product);
    // Task<bool> DoesProductExist(string name, string description);
    
    // add the other method definitions here
    
    Task<ProductDTO> GetProduct(int id);
    
    Task<ProductDTO> AddStock(ProductDTO product);
    // ----------------------------------------------------------------------------------------------------------------
    void ValidateProduct(ProductDTO product); // FluentValidation
}