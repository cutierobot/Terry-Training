using System.ComponentModel.DataAnnotations;

namespace TerryTraining.Persistence.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    public int CustomerId { get; set; } // FK
    public bool Completed { get; set; } 
    
    public Customer Customer { get; set; }
    
    public ICollection<OrderLine> OrderLines { get; }
}