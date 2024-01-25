using Infrastructure.Query;

namespace BusinessLayer.DTOs;

public class BaseFilterDto
{
    public int? PageNumber { get; set; } = PagingParameters.defaultPageNumber;
    public int? PageSize { get; set; } = PagingParameters.defaultPageSize;
    public string? SortParameter { get; set; }
    public bool? SortAscending { get; set; }
}
