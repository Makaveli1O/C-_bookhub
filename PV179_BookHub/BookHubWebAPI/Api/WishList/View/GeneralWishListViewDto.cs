namespace BookHubWebAPI.Api.WishList.View;

public class GeneralWishListViewDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Descripton { get; set; }
}
