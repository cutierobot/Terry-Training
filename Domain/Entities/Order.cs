namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; } // FK
    public bool Completed { get; set; } 
}