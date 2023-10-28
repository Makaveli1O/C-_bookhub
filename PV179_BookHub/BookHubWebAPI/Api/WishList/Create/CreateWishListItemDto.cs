namespace BookHubWebAPI.Api.WishList.Create;

public class CreateWishListItemDto
{
    public long WishListId { get; set; }
    public long BookId { get; set; }
    public uint PreferencePriorty { get; set; }
}
