using BusinessLayer.DTOs.Book.View;

namespace MVC.Models.Author;

public class AuthorDetailViewModel
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Biography { get; set; }
    public IEnumerable<MinimalBookViewDto> Books { get; set; } = Enumerable.Empty<MinimalBookViewDto>();
}
