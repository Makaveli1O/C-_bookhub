using MVC.Models.Base;

namespace MVC.Models.WishList;

public class WishListSearchModel : BaseSearchModel<WishListSortParameters>
{
    public string? CONTAINS_Description { get; set; }
    public DateTime? GE_CreatedAt { get; set; }
    public DateTime? LE_CreatedAt { get; set; }
}
