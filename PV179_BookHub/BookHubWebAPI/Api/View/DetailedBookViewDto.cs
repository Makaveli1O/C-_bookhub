using DataAccessLayer.Models.Enums;
using System.Text.Json.Serialization;

namespace BookHubWebAPI.Api.View;

public class DetailedBookViewDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BookGenre BookGenre { get; set; }
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public IEnumerable<GeneralBookReviewViewDto>? Reviews { get; set; }
}
