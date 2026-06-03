namespace TerryTraining.Domain.Entities.OrderAggregate;

public class Order
{
    // OrderAggregate root
    // OrderLine is child

    // Aggregate → Repository → DTO → API
    
    public Customer CustomerDetails { get; private set; }

    public float Total { get; private set; }
    public bool Completed { get; private set; }
    
    // 1. It prevents external code from modifying the aggregate’s internal state
    // 2. It enforces the rule: “All changes must go through the aggregate root”
    private readonly List<OrderLine> _products = new();
    public IReadOnlyCollection<OrderLine> Products => _products.AsReadOnly();
    // public ICollection<OrderLine> Products { get; set; } = new List<OrderLine>();
    
    public Order()
    {
        // set initialisation values
        Total = (float)00.00;
        Completed = false;
    }

    public void AddOrderLine(int productId, string productName, int quantity, float unitPrice)
    {
        if (_products.Count > 0 &  _products.Any(orderLine => orderLine.UnitPrice == 0))
        {
            throw new InvalidOperationException("Order already contains a product for free");
        }
        
        _products.Add(new OrderLine(productId, productName, quantity, unitPrice));
    }

    public void RemoveOrderLine(int productId)
    {
        _products.RemoveAll(x => x.ProductId == productId);
    }

    public void AddProductQuantity(int productId, int quantity)
    {
        // _items.FirstOrDefault(x => x.ItemName.Equals(itemName))?.AddQuantity(quantity);
        _products.FirstOrDefault(orderLine => orderLine.ProductId.Equals(productId))?.AddQuantity(quantity);
    }

    public void RemoveProductQuantity(int productId, int quantity)
    {
        _products.FirstOrDefault(orderLine => orderLine.ProductId.Equals((productId)))?.UpdateQuantity(quantity);
    }

    public void AddUpdateProductUnitPrice(int productId, float unitPrice)
    {
        _products.FirstOrDefault(orderLine => orderLine.ProductId.Equals(productId))?.UpdateUnitPrice(unitPrice);
    }

    public void MarkOrderAsCompleted()
    {
        this.Completed = true;
    }
    
}