using BusinessLayer.DTOs.Book.View;

namespace MVC.Models.Publisher;

public class PublisherDetailViewModel
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public ushort YearFounded { get; set; }
    public IEnumerable<MinimalBookViewDto> Books { get; set; } = Enumerable.Empty<MinimalBookViewDto>();
}
