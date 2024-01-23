using BusinessLayer.DTOs.Book.Create;
using DataAccessLayer.Models.Enums;

namespace MVC.Models.Book;

public class CreateBookModel
{
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public long PublisherId { get; set; }
    public long PrimaryAuthorId { get; set; }
    public IEnumerable<long>? AuthorIds { get; set; }
    public BookGenre BookGenre { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
}
