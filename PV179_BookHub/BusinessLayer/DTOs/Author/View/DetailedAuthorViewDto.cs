using BusinessLayer.DTOs.Book.View;

namespace BusinessLayer.DTOs.Author.View;

public class DetailedAuthorViewDto : GeneralAuthorViewDto
{
    public string? Biography { get; set; }
    public IEnumerable<GeneralBookViewDto>? Books { get; set; }
}
