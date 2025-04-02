namespace TerryTraining.Domain.Entities;

public class OrderLine
{
    public int Id { get; set; }
    public int OrderId { get; set; } // FK
    public int ProductId { get; set; } // FK
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
}