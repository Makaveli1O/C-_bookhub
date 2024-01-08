using BusinessLayer.DTOs.WishList.View;

namespace MVC.Models.WishList;

public class WishListUpdateViewModel
{
    public string? Description { get; set; }
    public IEnumerable<WishListItemViewModel>? WishListItems { get; set; }
    public IEnumerable<WishListAvailableBooksViewModel>? AvailableBooks { get; set; }
}
