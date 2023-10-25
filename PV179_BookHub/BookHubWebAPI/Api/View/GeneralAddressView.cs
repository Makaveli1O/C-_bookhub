namespace BookHubWebAPI.Api.View;

public class GeneralAddressView
{
    public required string City { get; set; }
    public string? Street { get; set; }
    public string? StreetNumber { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
}
