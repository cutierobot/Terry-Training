using System.ComponentModel.DataAnnotations;

namespace TerryTraining.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    public int CustomerId { get; set; } // FK
    public bool Completed { get; set; } 
}