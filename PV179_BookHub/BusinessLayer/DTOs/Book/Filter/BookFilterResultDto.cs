using BusinessLayer.DTOs.Book.View;

namespace BusinessLayer.DTOs.Book.Filter;

public class BookFilterResultDto
{
    public long TotalItemsCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public IList<GeneralBookViewDto> Books { get; set; } = new List<GeneralBookViewDto>();
    public bool PagingEnabled { get; set; }
}
