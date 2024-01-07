namespace BusinessLayer.DTOs.WishList.Create;

public class CreateWishListItemDto
{
    public long WishListId { get; set; }
    public long BookId { get; set; }
    public uint PreferencePriority { get; set; }
}
