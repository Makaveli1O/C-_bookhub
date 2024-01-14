namespace BusinessLayer.DTOs.Address.View;

public class GeneralAddressView
{
    public long Id { get; set; }
    public required string City { get; set; }
    public required string Street { get; set; }
    public required string State { get; set; }
}
