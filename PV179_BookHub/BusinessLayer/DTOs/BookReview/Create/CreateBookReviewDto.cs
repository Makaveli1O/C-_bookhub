using DataAccessLayer.Models.Enums;

namespace BusinessLayer.DTOs.BookReview.Create;

public class CreateBookReviewDto
{
    public required long BookId { get; set; }
    public required long ReviewerId { get; set; }
    public string Description { get; set; }
    public required Rating Rating { get; set; }
}
