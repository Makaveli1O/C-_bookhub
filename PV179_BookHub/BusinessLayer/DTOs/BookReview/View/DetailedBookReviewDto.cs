using DataAccessLayer.Models.Enums;
using BusinessLayer.DTOs.User.View;

namespace BusinessLayer.DTOs.BookReview.View;

public class DetailedBookReviewViewDto
{
    public long Id { get; set; }
    public long BookId { get; set; }
    public long ReviewerId { get; set; }
    public GeneralUserViewDto? Reviewer { get; set; }
    public string Description { get; set; }
    public Rating Rating { get; set; }
}
