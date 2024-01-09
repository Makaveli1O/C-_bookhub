namespace BusinessLayer.DTOs;

public class BaseFilterDto
{
    public int? PageNumber { get; set; } = 1;
    public int? PageSize { get; set; } = 20;
    public string? SortParameter { get; set; }
    public bool? SortAscending { get; set; }
}
