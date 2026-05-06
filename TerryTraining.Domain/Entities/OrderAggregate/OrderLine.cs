namespace TerryTraining.Domain.Entities.OrderAggregate;

public class OrderLine
{
    public int ProductId { get; private set; } 
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public int OrderLineId { get; private set; } // Wasn't included in the graph but think Terry might have just accidentally ommited
    public float UnitPrice { get; private set; } 
    // public float LineTotal { get; private set; }
    
    public float LineTotal => Quantity * UnitPrice;

    internal OrderLine(int productId, string productName, int quantity, float unitPrice)
    {
        
    }
}