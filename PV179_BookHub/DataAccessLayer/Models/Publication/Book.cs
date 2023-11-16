using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Enums;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Purchasing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.Publication;

public class Book : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string? Title { get; set; }
    [Required]
    [MaxLength(15)]
    public string? ISBN { get; set; }
    public long PublisherId { get; set; }
    [ForeignKey(nameof(PublisherId))]
    public virtual Publisher? Publisher { get; set; }
    [Required]
    public BookGenre BookGenre { get; set; }
    [MaxLength(700)]
    public string? Description { get; set; }
    public double Price { get; set; }

    public virtual IEnumerable<BookReview>? Reviews { get; set; }
    public virtual IEnumerable<InventoryItem>? InventoryItems { get; set; }
    public virtual IEnumerable<OrderItem>? OrderItems { get; set; }
    public virtual IEnumerable<WishListItem>? WishListItems { get; set; }
    public virtual IEnumerable<Author>? Authors { get; set; }
}
