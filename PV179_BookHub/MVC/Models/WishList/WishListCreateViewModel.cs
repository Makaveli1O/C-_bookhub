namespace MVC.Models.WishList;

public class WishListCreateViewModel
{
    public string? Description { get; set; }
    public IEnumerable<WishListItemViewModel>? WishListItems { get; set; }
}
