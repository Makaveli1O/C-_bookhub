using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class WishListItem : BaseEntity
{
    public long WishListId { get; set; }
    [ForeignKey(nameof(WishListId))]
    public virtual WishList? WishList { get; set; }
    public long BookId { get; set; }
    [ForeignKey(nameof(BookId))]
    public virtual Book? Book { get; set; }
    public uint PreferencePriorty { get; set; }
}
