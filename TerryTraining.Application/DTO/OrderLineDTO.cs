namespace TerryTraining.Application.DTO;

public class OrderLineDTO
{
    public int Id { get; set; } // PK
    public int OrderId { get; set; } // FK
    public int ProductId { get; set; } // FK
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    
    public OrderDTO Order { get; set; }
    public ProductDTO Product { get; set; }
}
