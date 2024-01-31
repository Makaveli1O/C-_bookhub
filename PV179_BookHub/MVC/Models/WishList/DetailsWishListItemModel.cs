using BusinessLayer.DTOs.Book.View;

namespace MVC.Models.WishList;

public class DetailsWishListItemModel
{
    public long Id { get; set; }
    public long WishListId { get; set; }
    public MinimalBookViewDto? Book { get; set; }
    public uint PreferencePriority { get; set; }
}
