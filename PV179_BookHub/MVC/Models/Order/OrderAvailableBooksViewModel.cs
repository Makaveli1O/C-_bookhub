namespace MVC.Models.Order;

public class OrderAvailableBooksViewModel
{
    public required long BookId { get; set; }
    public required string Title { get; set; }
    public required double Price { get; set; }
    public required uint InStock { get; set; }
    public required long BookStoreId {  get; set; }
}
