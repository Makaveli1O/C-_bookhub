using DataAccessLayer.Models.Enums;

namespace BookHubWebAPI.Api.Create
{
    public class CreateBookReviewDto
    {
        public long BookId { get; set; }
        public long ReviewerId { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
    }
}
