using DataAccessLayer.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class User : BaseEntity
    {
        [MaxLength(100)]
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public UserRole Role { get; set; }
        //public virtual IEnumerable<BookReview>? BookReviews { get; set; }
        //public virtual IEnumerable<WishList>? WishLists { get; set; }
        //public virtual IEnumerable<Order>? Orders { get; set; }
    }
}
