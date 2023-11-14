using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Publication;

public class Author : BaseEntity
{
    [Required]
    [MaxLength(70)]
    public string? Name { get; set; }

    public virtual IEnumerable<AuthorBookAssociation>? Associations { get; set; }
}
