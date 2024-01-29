using DataAccessLayer.Models.Enums;

namespace MVC.Models.BookReview;

public class BookReviewEditViewModel
{
    public long UserId { get; set; }
    public long Id { get; set; }
    public string Description { get; set; }
    public Rating rating { get; set; }
}
