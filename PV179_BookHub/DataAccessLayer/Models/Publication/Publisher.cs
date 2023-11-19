using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Publication;

public class Publisher : BaseEntity
{
    [Required]
    [MaxLength(70)]
    public string? Name { get; set; }
    [MaxLength(30)]
    public string? City { get; set; }
    [MaxLength(30)]
    public string? Country { get; set; }
    public ushort YearFounded { get; set; }

    public virtual IEnumerable<Book>? Books { get; set; }
}
