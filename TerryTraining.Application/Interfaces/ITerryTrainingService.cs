namespace TerryTraining.Application.Interfaces;

public interface ITerryTrainingService
{
    // returning string is only temp for testing
    string NewProduct(string name, string description, int stockcount);
    
    // add the other method definitions here
}