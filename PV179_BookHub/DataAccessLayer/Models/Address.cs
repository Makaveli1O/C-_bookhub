using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class Address : BaseEntity
{
    [MaxLength(85)]
    public required string City { get; set; }
    [MaxLength(100)]
    public string? Street { get; set; }
    [MaxLength(25)]
    public string? StreetNumber { get; set; }
    [MaxLength(20)]
    public required string State { get; set; }
    [MaxLength(10)]
    public required string PostalCode { get; set; }
}
