using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class WishList : BaseEntity
{
    public long UserId { get; set; }
    /* @todo  Uncomment when User entity added */
    //[ForeignKey(nameof(UserId))]
    //public virtual User? Creator {  get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [MaxLength(500)]
    public string? Description {  get; set; }
    public virtual IEnumerable<WishListItem>? WishListItems { get; set; }
}
