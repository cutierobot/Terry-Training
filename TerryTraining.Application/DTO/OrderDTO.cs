namespace TerryTraining.Application.DTO;

public class OrderDTO
{
    public int Id { get; set; }
    public int CustomerId { get; set; } // FK
    public bool Completed { get; set; } 
    
    public CustomerDTO Customer { get; set; }
    
    public ICollection<OrderLineDTO> OrderLines { get; }
}