using BusinessLayer.DTOs.Book.View;

namespace MVC.Models.WishList;

public class WishListItemViewModel
{
    public long Id { get; set; }
    public long WishListId { get; set; }
    public GeneralBookViewDto? Book { get; set; }
}
