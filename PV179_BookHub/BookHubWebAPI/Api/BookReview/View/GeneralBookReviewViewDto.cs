using DataAccessLayer.Models.Enums;

namespace BookHubWebAPI.Api.BookReview.View;

public class GeneralBookReviewViewDto
{
    public long BookId { get; set; }
    public long ReviewerId { get; set; }
    public string Description { get; set; }
    public Rating Rating { get; set; }
}
