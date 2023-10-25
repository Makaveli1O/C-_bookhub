using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class Address : BaseEntity
{
    [MaxLength(100)]
    public string? Street;
    [MaxLength(25)]
    public string? StreetNumber;
    [MaxLength(85)]
    public required string City;
    [MaxLength(20)]
    public required string State;
    [MaxLength(10)]
    public required string PostalCode;
}
