using DataAccessLayer.Models.Enums;

namespace MVC.Models.BookReview;

public class BookReviewCreateViewModel
{
    public long BookId { get; set; }
    public long SelectedBook {  get; set; }
    public required long ReviewerId { get; set; }
    public string Description { get; set; }
    public HashSet<BookReviewAvailableBooksViewModel> AvailableBooks { get; set; } = new HashSet<BookReviewAvailableBooksViewModel>();
    public Rating Rating { get; set; }
}
