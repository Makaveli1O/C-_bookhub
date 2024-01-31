using BusinessLayer.DTOs.Book.View;

namespace BusinessLayer.DTOs.Publisher.View;

public class DetailedPublisherViewDto : GeneralPublisherViewDto
{
    public IEnumerable<MinimalBookViewDto>? Books { get; set; }
}
