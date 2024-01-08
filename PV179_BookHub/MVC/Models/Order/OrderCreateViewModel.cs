namespace MVC.Models.Order;

public class OrderCreateViewModel
{
    public long BookId { get; set; }
    public long BookStoreId { get; set; }
    public uint Quantity { get; set; }
}
