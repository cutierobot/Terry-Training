using System.ComponentModel.DataAnnotations;

namespace TerryTraining.Persistence.Models;

public class OrderLine
{
    [Key]
    public int Id { get; set; }
    public int OrderId { get; set; } // FK
    public int ProductId { get; set; } // FK
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    
    public Order Order { get; set; }
    public Product Product { get; set; }
}