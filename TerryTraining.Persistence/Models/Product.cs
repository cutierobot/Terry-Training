using System.ComponentModel.DataAnnotations;

namespace TerryTraining.Persistence.Models;

public class Product
{
    [Key]
    public int Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public int Reserved { get; set; }
    
    public virtual ICollection<OrderLine> OrderLines { get; }
}