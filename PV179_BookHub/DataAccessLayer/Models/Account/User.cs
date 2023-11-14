using DataAccessLayer.Models.Enums;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Purchasing;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Account;

public class User : BaseEntity
{
    [MaxLength(100)]
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    public virtual IEnumerable<BookReview>? BookReviews { get; set; }
    public virtual IEnumerable<WishList>? WishLists { get; set; }
    public virtual IEnumerable<Order>? Orders { get; set; }
    public virtual BookStore? BookStore { get; set; }
}
