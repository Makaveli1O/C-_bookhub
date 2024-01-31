namespace MVC.Models.Book;

public class UnAssignAuthorViewModel
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public long AuthorId { get; set; }
}
