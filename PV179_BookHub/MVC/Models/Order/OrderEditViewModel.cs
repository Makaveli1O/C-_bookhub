using BusinessLayer.DTOs.BookStore.View;
using DataAccessLayer.Models.Logistics;

namespace MVC.Models.Order;

public class OrderEditViewModel
{
    public IList<OrderBookStoresViewModel> AvailableBookStores { get; set; }
    public int SelectedBookStore { get; set; }
    public List<int> AddedOrderItems { get; set; } = new List<int>();
    public Dictionary<int, int> AddedItems { get; set; } = new Dictionary<int, int>();
    public List<int> RemovedOrderItems { get; set; } = new List<int>();
    public IList<OrderItemViewModel>? OrderItems { get; set; }
    public IList<OrderAvailableBooksViewModel>? AvailableBooks { get; set; }
}