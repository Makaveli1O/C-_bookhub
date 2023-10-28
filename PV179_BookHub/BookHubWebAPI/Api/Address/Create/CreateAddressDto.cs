namespace BookHubWebAPI.Api.Address.Create;

public class CreateAddressDto
{
    public required string City { get; set; }
    public string? Street { get; set; }
    public string? StreetNumber { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
}
