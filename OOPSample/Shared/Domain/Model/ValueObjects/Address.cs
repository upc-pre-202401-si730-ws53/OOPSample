namespace OOPSample.Shared.Domain.Model.ValueObjects;

public record Address(string Street, string City, string State, string ZipCode, string Country)
{
    public string AddressAsString => $"{Street}, {City}, {State}, {ZipCode}, {Country}";
}