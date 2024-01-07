using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.Publication;

public class AuthorBookAssociation : BaseEntity
{
    [Required]
    public long AuthorId { get; set; }
    [ForeignKey(nameof(AuthorId))]
    public virtual Author? Author { get; set; }
    [Required]
    public virtual long BookId { get; set; }
    [ForeignKey(nameof(BookId))]
    public virtual Book? Book { get; set; }
}
