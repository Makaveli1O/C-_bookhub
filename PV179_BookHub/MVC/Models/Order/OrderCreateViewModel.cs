namespace MVC.Models.Order;

public class OrderCreateViewModel
{
    public IList<OrderBookStoresViewModel> AvailableBookStores { get; set; }
    public int SelectedBookStore { get; set; }
    public List<int> AddedOrderItems { get; set; } = new List<int>();
    public Dictionary<int, uint> AddedItems { get; set; } = new Dictionary<int, uint>();
    public List<int> RemovedOrderItems { get; set; } = new List<int>();
    public IList<OrderItemViewModel>? OrderItems { get; set; }
    public IList<OrderAvailableBooksViewModel>? AvailableBooks { get; set; }
}
