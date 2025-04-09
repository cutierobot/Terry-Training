namespace TerryTraining.Application.DTO;

public class ProductDTO
{
    public int Id { get; set; } //PK
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public int Reserved { get; set; }
    
    public ICollection<OrderLineDTO> OrderLines { get; }
}
