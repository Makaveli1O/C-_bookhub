using DataAccessLayer.Models.Enums;

namespace BusinessLayer.DTOs.Book.Create;

public class CreateBookDto
{
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public long PublisherId { get; set; }
    public IEnumerable<AuthorBookAssociationDto>? AuthorIds { get; set; }
    public BookGenre BookGenre { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
}
