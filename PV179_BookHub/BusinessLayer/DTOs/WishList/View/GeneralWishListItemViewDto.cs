using BusinessLayer.DTOs.Book.View;

namespace BusinessLayer.DTOs.WishList.View;

public class GeneralWishListItemViewDto
{
    public long Id { get; set; }
    public long WishListId { get; set; }
    public MinimalBookViewDto? Book { get; set; }
    public uint PreferencePriority { get; set; }
}
