using DataAccessLayer.Models.Enums;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Purchasing;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models.Account;

public class User : IdentityUser<long>
{
    public UserRole Role { get; set; } = UserRole.User;
    public virtual IEnumerable<BookReview>? BookReviews { get; set; }
    public virtual IEnumerable<WishList>? WishLists { get; set; }
    public virtual IEnumerable<Order>? Orders { get; set; }
    public virtual BookStore? BookStore { get; set; }
}
