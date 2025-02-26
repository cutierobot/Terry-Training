using System.ComponentModel.DataAnnotations;

namespace TerryTraining.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public int Reserved { get; set; }
    
    public ICollection<OrderLine> OrderLines { get; }
}