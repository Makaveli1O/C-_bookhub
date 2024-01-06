namespace Infrastructure.Query;

public class QueryParams
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string SortParameter { get; set; } = string.Empty;
    public bool SortAscending { get; set; } = false;
}
