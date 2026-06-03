namespace TerryTraining.Domain.Entities.OrderAggregate;

public class OrderLine
{
    public int ProductId { get; private set; } 
    // set and never update ProductName. this is so product matches the product name at time of receipt, so if product name changes still accurate 
    // as time receipt was issued. no user input for name
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public int OrderLineId { get; private set; } // Wasn't included in the graph but think Terry might have just accidentally ommited
    public float UnitPrice { get; private set; } 
    
    public float LineTotal => Quantity * UnitPrice;

    
    // internal 
    // allows creation of it inside this entity but not outside of it
    
    internal OrderLine(int productId, string productName, int quantity, float unitPrice)
    {
        // Only check and handle internal OrderLine specific buisness logic. Checking that a product exists is not this 
        // classes responsibility as it is external to OrderLine. by the time productId gets here it should already have been validated
        // as being a valid productId.
        
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
        
        if (unitPrice < 0)
        {
            throw new ArgumentException("unitPrice can not be negative.", nameof(unitPrice));
        }

        if (unitPrice == 0 && quantity > 1)
        {
            throw new ArgumentException("unitPrice can only be 0 if the quantity is 1.", nameof(unitPrice));
        }

        if (productName == null)
        {
            throw new ArgumentNullException("productName cannot be null.", nameof(productName));
        }

        if (productName.Length == 0)
        {
            throw new ArgumentException("productName cannot be empty.", nameof(productName));
        }
        
        
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    internal void AddQuantity(int quantity)
    {
        this.Quantity += quantity;
    }

    internal void UpdateQuantity(int quantity)
    {
        if (quantity >= this.Quantity)
        {
            throw new InvalidOperationException("Can't remove all units. Remove the entire item instead.");
        }
        this.Quantity -= quantity;
    }
    
    internal void UpdateUnitPrice(float unitPrice)
    {
        // allowing to set to 0 as product might be given for free
        if (unitPrice < 0)
        {
            throw new InvalidOperationException("Can't remove all units. Remove the entire item instead.");
        }
        
        // Buisness Rule: UnitPrice of $0 is only valid if the quantity is 1. A whole Order can only have 1 free item.
        if (unitPrice == 0 && this.Quantity > 1)
        {
            throw new InvalidOperationException("unitPrice can only be 0 if the quantity is 1.");
        }
        
        
        this.UnitPrice = unitPrice;
    }
}