using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Logistics;

public class Address : BaseEntity
{
    [MaxLength(30)]
    public required string City { get; set; }
    [MaxLength(30)]
    public string? Street { get; set; }
    [MaxLength(10)]
    public string? StreetNumber { get; set; }
    [MaxLength(20)]
    public required string State { get; set; }
    [MaxLength(10)]
    public required string PostalCode { get; set; }
    public virtual BookStore? BookStore { get; set; }
}
