namespace OOPSample.Sales.Domain.Model.Aggregates;

public class SalesOrderItem
{
    public SalesOrderItem(Guid salesOrderId, int productId, int quantity, double unitPrice)
    {
        if (salesOrderId == Guid.Empty)
        {
            throw new ArgumentException("Sales order id is required.");
        }
        
        if (productId <= 0)
        {
            throw new ArgumentException("Product id is required.");
        }
        
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }
        
        if (unitPrice <= 0)
        {
            throw new ArgumentException("Unit price must be greater than zero.");
        }
        SalesOrderId = salesOrderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public Guid Id { get; private set; } = GenerateOrderItemId();
    public Guid SalesOrderId { get;  }
    public int ProductId { get; }
    public int Quantity { get;  }
    public double UnitPrice { get; }

    private static Guid GenerateOrderItemId()
    {
        return Guid.NewGuid();
    }
}