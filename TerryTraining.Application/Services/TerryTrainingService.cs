using TerryTraining.Application.Interfaces;
using TerryTraining.Persistence;

namespace TerryTraining.Application.Services;

public class TerryTrainingService: ITerryTrainingService
{
    private readonly TerryDbContext _context;

    public TerryTrainingService(TerryDbContext context)
    {
        _context = context;
    }
    public string NewProduct(string name, string description, int stockcount)
    {
        //  Check doesn’t already exist
        //  Check name, description sizes
        //  Check stockcount > 0
        throw new NotImplementedException("NewProduct is not implemented yet."); 
    }
}