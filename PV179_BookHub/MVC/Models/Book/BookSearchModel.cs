using DataAccessLayer.Models.Enums;
using MVC.Models.Base;

namespace MVC.Models.Book;

public class BookSearchModel : BaseSearchModel<BookSortParam>
{
    public string? CONTAINS_Title { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public string? CONTAINS_Description { get; set; }
    public string? CONTAINS_ISBN { get; set; }
    public BookGenre? BookGenre { get; set; }
    public double? LE_Price { get; set; }
    public double? GE_Price { get; set; }
}
