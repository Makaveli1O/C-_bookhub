using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Publication;

public class Author : BaseEntity
{
    [Required]
    [MaxLength(70)]
    public string? Name { get; set; }
    [MaxLength(200)]
    public string? Biography { get; set; }

    public virtual IEnumerable<Book>? Books { get; set; }
}
