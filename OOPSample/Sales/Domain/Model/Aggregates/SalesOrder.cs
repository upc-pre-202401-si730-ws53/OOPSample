using OOPSample.Shared.Domain.Model.ValueObjects;

namespace OOPSample.Sales.Domain.Model.Aggregates;

public class SalesOrder(int customerId)
{
    public Guid Id { get; } = Guid.NewGuid();
    public int CustomerId { get; } = customerId;
    public SalesOrderStatus Status { get; private set; } = SalesOrderStatus.PendingPayment;
    public Address ShippingAddress { get; private set; }
    public double PaidAmount { get; private set; } = 0;

}