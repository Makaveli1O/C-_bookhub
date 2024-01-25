using DataAccessLayer.Models.Enums;

namespace BusinessLayer.DTOs.Book.Filter;

public class BookFilterDto : BaseFilterDto
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
