using BusinessLayer.DTOs.Author.View;
using DataAccessLayer.Models.Enums;
using DataAccessLayer.Models.Publication;

namespace BusinessLayer.DTOs.BookRecommendation.View;

public class RecommendationUserProfile
{
    public int UserId { get; set; }
    public List<BookGenre> PreferredGenres { get; set; } = new List<BookGenre>();
    public List<long> PreferredAuthors { get; set; } = new List<long>();
}
