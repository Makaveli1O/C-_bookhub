using BusinessLayer.DTOs.Book.Create;
using DataAccessLayer.Models.Enums;

namespace BusinessLayer.DTOs.Book.Update;

public class UpdateBookDto
{
    public string? Title { get; set; }
    public string? ISBN { get; set; }
    public long? PublisherId { get; set; }
    public BookGenre? BookGenre { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
}
