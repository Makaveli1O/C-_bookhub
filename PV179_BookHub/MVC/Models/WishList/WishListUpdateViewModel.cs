using MVC.Models.WishList;

public class WishListUpdateViewModel
{
    public string Description { get; set; }
    public List<int> AddedBooks { get; set; } = new List<int>();
    public List<int> RemovedBooks { get; set; } = new List<int>();
    public IList<WishListItemViewModel>? WishListItems { get; set; }
    public IList<WishListAvailableBooksViewModel>? AvailableBooks { get; set; }
}