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

    
    // internal 
    // allows creation of it inside this entity but not outside of it
    
    internal OrderLine(int productId, int quantity, float unitPrice)
    {
        
        // check product exists
        // qunatity is bigger then 0

        // Terry how do i call GetProducts here that returns null if prduct does not exist, i need ProductService but not
        // sure how to add it here help
        if (productId == 0)
        {
            throw new ArgumentException("productId must be at least one.", nameof(productId));
        }

        if (quantity == 0)
        {
            throw new ArgumentException("Quantity must be at least one.", nameof(quantity));
        }
        
        if (unitPrice == 0)
        {
            throw new ArgumentException("unitPrice must be at least one.", nameof(unitPrice));
        }
        
        
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    internal void AddQuantity(int quantity)
    {
        this.Quantity += quantity;
    }

    internal void WithdrawQuantity(int quantity)
    {
        if (quantity >= this.Quantity)
        {
            throw new InvalidOperationException("Can't remove all units. Remove the entire item instead.");
        }
        this.Quantity -= quantity;
    }
}