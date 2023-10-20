using DataAccessLayer.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Models;

public class Book : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public required string Title { get; set; }
    [MaxLength(50)]
    public string? Author { get; set; }
    [MaxLength(50)]
    public string? Publisher { get; set; }
    [Required]
    [MaxLength(30)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BookGenre BookGenre { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    public double Price { get; set; }
}
