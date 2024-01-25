using DataAccessLayer.Models.Enums;
using Infrastructure.Query;

namespace MVC.Models.Book;

public class BookSearchModel
{
    public string? CONTAINS_Title { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public string? CONTAINS_Description { get; set; }
    public string? CONTAINS_ISBN { get; set; }
    public BookGenre? BookGenre { get; set; }
    public double? LE_Price { get; set; }
    public double? GE_Price { get; set; }
    public BookSortParam? SortParameter { get; set; }
    public bool SortAscending { get; set; }

    public int? PageNumber { get; set; } = PagingParameters.defaultPageNumber;
    public int? PageSize { get; set; } = PagingParameters.defaultPageSize;
}
