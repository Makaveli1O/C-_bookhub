using BusinessLayer.DTOs.Author.View;
using BusinessLayer.DTOs.BookReview.View;
using BusinessLayer.DTOs.Publisher.View;
using DataAccessLayer.Models.Enums;
using System.Text.Json.Serialization;

namespace BusinessLayer.DTOs.Book.View;

public class DetailedBookViewDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public GeneralAuthorViewDto? PrimaryAuthor { get; set; }
    public IEnumerable<GeneralAuthorViewDto>? CoAuthors { get; set; }
    public GeneralPublisherViewDto? Publisher { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BookGenre BookGenre { get; set; }
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public IEnumerable<DetailedBookReviewViewDto>? Reviews { get; set; }
}
