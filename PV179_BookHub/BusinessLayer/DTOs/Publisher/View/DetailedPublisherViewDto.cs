

using BusinessLayer.DTOs.Book.View;

namespace BusinessLayer.DTOs.Publisher.View;

public class DetailedPublisherViewDto : GeneralPublisherViewDto
{
    public string? City { get; set; }
    public string? Country { get; set; }
    public ushort YearFounded { get; set; }
    public IEnumerable<GeneralBookViewDto>? Books { get; set; }
}
