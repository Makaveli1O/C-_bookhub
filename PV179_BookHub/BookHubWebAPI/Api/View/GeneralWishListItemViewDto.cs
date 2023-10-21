using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Api.View;

public class GeneralWishListItemViewDto
{
    public long Id { get; set; }
    public long WishListId { get; set; }
    public long BookId { get; set; }
    public int PreferencePriorty { get; set; }
}
