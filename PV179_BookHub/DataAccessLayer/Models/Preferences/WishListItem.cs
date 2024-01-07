using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Models.Publication;

namespace DataAccessLayer.Models.Preferences;

public class WishListItem : BaseEntity
{
    public long WishListId { get; set; }
    [ForeignKey(nameof(WishListId))]
    public virtual WishList? WishList { get; set; }
    public long BookId { get; set; }
    [ForeignKey(nameof(BookId))]
    public virtual Book? Book { get; set; }
    public uint PreferencePriority { get; set; }
}
