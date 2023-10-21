namespace BookHubWebAPI.Api.Create;

public class CreateWishListItemDto
{
    public long WishListId { get; set; }
    public long BookId { get; set; }
    public int PreferencePriorty { get; set; }
}
