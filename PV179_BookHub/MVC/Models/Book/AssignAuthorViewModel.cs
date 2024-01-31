using BusinessLayer.DTOs.Author.View;

namespace MVC.Models.Book;

public class AssignAuthorViewModel
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public GeneralAuthorViewDto? PrimaryAuthor { get; set; }
    public IEnumerable<GeneralAuthorViewDto>? CoAuthors { get; set; }
    public long AuthorId { get; set; }
    public bool IsPrimary { get; set; }
    public bool Force { get; set; }
}
