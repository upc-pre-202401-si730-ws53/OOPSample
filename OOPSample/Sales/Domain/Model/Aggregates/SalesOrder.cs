namespace OOPSample.Sales.Domain.Model.Aggregates;

public class SalesOrder(int customerId)
{
    public Guid Id { get; } = Guid.NewGuid();
    public int CustomerId { get; } = customerId;
    
    
}