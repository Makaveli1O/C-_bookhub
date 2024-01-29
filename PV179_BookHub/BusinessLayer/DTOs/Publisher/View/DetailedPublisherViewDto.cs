using BusinessLayer.DTOs.Book.View;

namespace BusinessLayer.DTOs.Publisher.View;

public class DetailedPublisherViewDto : GeneralPublisherViewDto
{
    public IEnumerable<GeneralBookViewDto>? Books { get; set; }
}
