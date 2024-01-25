using Infrastructure.Query;

namespace MVC.Models.Base;

public abstract class BaseSearchModel<TEnum>
{
    public TEnum? SortParameter { get; set; }
    public bool SortAscending { get; set; }
    public int? PageNumber { get; set; } = PagingParameters.defaultPageNumber;
    public int? PageSize { get; set; } = PagingParameters.defaultPageSize;
}
