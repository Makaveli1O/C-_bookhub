using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Publication;

public class Publisher : BaseEntity
{
    [Required]
    [MaxLength(70)]
    public string? Name { get; set; }

    public virtual IEnumerable<Book>? Books { get; set; }
}
