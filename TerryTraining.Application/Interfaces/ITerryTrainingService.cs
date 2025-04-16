using TerryTraining.Application.DTO;

namespace TerryTraining.Application.Interfaces;

public interface ITerryTrainingService
{
    // returning string is only temp for testing
    // string NewProÎduct(string name, string description, int stockcount);
    Task<ProductDTO> NewProduct(string name, string description, int stockcount);
    // Task<bool> DoesProductExist(string name, string description);
    
    // add the other method definitions here
    
    Task<ProductDTO> GetProduct(int id);
}