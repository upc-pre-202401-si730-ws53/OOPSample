using OOPSample.Shared.Domain.Model.ValueObjects;

namespace OOPSample.Sales.Domain.Model.Aggregates;

public class SalesOrder(int customerId)
{
    public Guid Id { get; } = Guid.NewGuid();
    public int CustomerId { get; } = customerId;
    public SalesOrderStatus Status { get; private set; } = SalesOrderStatus.PendingPayment;
    public Address ShippingAddress { get; private set; }
    public double PaidAmount { get; private set; } = 0;
    private readonly List<SalesOrderItem> _items = [];

    public void AddItem(int productId, int quantity, double unitPrice)
    {
        if (Status != SalesOrderStatus.PendingPayment)
            throw new InvalidOperationException("Can't modify an order once payment is processed.");
        _items.Add(new SalesOrderItem(Id, productId, quantity, unitPrice));
    }

    public void Cancel()
    {
        Status = SalesOrderStatus.Cancelled;
    }

    public void Dispatch(string street, string city, string state, string zipCode, string country)
    {
        if (Status == SalesOrderStatus.PendingPayment)
            throw new InvalidOperationException("Can't dispatch and order that is not paid yet.");
        
        if (_items.Count == 0)
            throw new InvalidOperationException("Can't dispatch and order without items.");
        
        ShippingAddress = new Address(street, city, state, zipCode, country);
        Status = SalesOrderStatus.Shipped;
    }

    public double CalculateTotalPrice() => _items.Sum(item => item.CalculateItemPrice());

    public void AddPayment(double amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Amount must be greater than zero.");
        
        if (amount > CalculateTotalPrice() - PaidAmount)
            throw new InvalidOperationException("Amount must be less than or equal to the total price.");
        
        PaidAmount += amount;
        VerifyIfReadyForShipment();
    }

    private void VerifyIfReadyForShipment()
    {
        if (PaidAmount == CalculateTotalPrice())
            Status = SalesOrderStatus.ReadyForShipment;
    }
}