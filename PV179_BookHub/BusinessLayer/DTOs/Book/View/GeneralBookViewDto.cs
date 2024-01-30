using BusinessLayer.DTOs.Author.View;
using BusinessLayer.DTOs.Publisher.View;
using DataAccessLayer.Models.Enums;
using System.Text.Json.Serialization;

namespace BusinessLayer.DTOs.Book.View;

public class GeneralBookViewDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public string? ISBN { get; set; }
    public GeneralAuthorViewDto? Author { get; set; }
    public GeneralPublisherViewDto? Publisher { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BookGenre BookGenre { get; set; }
    public double Price { get; set; }
}
