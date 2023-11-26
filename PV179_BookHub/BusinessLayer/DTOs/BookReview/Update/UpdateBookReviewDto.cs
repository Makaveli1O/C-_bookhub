using DataAccessLayer.Models.Enums;

namespace BusinessLayer.DTOs.BookReview.Update;

public class UpdateBookReviewDto
{
    public string Description { get; set; }
    public Rating Rating { get; set; }
}
