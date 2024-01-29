using DataAccessLayer.Models.Enums;

namespace MVC.Models.BookReview;

public class BookReviewViewModel
{
    public string Title { get; set; }
    public string ReviewerName { get; set; }
    public string Description { get; set; }
    public Rating Rating { get; set; }
}