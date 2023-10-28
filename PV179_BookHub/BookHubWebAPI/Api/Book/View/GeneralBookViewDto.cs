using DataAccessLayer.Models.Enums;
using System.Text.Json.Serialization;

namespace BookHubWebAPI.Api.Book.View;

public class GeneralBookViewDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public string Author { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BookGenre BookGenre { get; set; }
    public double Price { get; set; }
}
