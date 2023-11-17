namespace BusinessLayer.DTOs.Address.View;

public class DetailedAddressView
{
    public long Id { get; set; }
    public required string City { get; set; }
    public string? Street { get; set; }
    public string? StreetNumber { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
}
