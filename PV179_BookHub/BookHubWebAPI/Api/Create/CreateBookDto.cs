using DataAccessLayer.Models.Enums;
using System.Text.Json.Serialization;

namespace BookHubWebAPI.Api.Create;

public class CreateBookDto
{
    public required string Title { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public BookGenre BookGenre { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
}
