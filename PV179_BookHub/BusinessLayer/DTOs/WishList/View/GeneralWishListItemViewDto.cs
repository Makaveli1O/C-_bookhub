namespace BusinessLayer.DTOs.WishList.View;

public class GeneralWishListItemViewDto
{
    public long Id { get; set; }
    public long WishListId { get; set; }
    public long BookId { get; set; }
    public uint PreferencePriorty { get; set; }
}
