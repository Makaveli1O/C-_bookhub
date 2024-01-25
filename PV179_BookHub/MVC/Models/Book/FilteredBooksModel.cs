using BusinessLayer.DTOs.Book.View;

namespace MVC.Models.Book;

public class FilteredBooksModel
{
    public BookSearchModel? SearchBooksModel { get; set; }
    public int TotalPages { get; set; }
    public int PageNumber { get; set; }
    public IEnumerable<GeneralBookViewDto> Books { get; set; } = new List<GeneralBookViewDto>();
}
