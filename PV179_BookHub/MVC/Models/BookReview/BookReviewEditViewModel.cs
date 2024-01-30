using DataAccessLayer.Models.Enums;

namespace MVC.Models.BookReview;

public class BookReviewEditViewModel
{
    public long ReviewerId { get; set; }
    public string BookTitle { get; set; }
    public long Id { get; set; }
    public string Description { get; set; }
    public Rating Rating { get; set; }
}
