using DataAccessLayer.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class Book : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    [Required]
    [MaxLength(15)]
    public string ISBN { get; set; }
    [MaxLength(50)]
    public string? Author { get; set; }
    [MaxLength(50)]
    public string? Publisher { get; set; }
    [Required]
    public BookGenre BookGenre { get; set; }
    [MaxLength(700)]
    public string? Description { get; set; }
    public double Price { get; set; }
}
