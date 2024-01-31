using DataAccessLayer.Models.Preferences;

namespace Infrastructure.Query.Filters.EntityFilters;

public class WishListFilter : FilterBase<WishList>
{
    protected override void SetUpSpecialLambdaExpressions()
    {
    }
    public long? UserId { get; set; }
    public string? CONTAINS_Description { get; set; }
    public DateTime? GE_CreatedAt { get; set; }
    public DateTime? LE_CreatedAt { get; set; }
}
