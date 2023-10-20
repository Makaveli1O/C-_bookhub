using DataAccessLayer.Models.Enums;

namespace BookHubWebAPI.Api.Create;

public class CreateBookDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public BookGenre BookGenre { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
}
