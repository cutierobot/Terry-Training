

using Application.Interfaces;

namespace Application.Services;

public class TerryTrainingService: ITerryTrainingService
{
    public string NewProduct(string name, string description, int stockcount)
    {
        //  Check doesn’t already exist
        //  Check name, description sizes
        //  Check stockcount > 0
        throw new NotImplementedException("NewProduct is not implemented yet."); 
    }
}