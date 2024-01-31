using DataAccessLayer.Models.Publication;

namespace Infrastructure.Query.Filters.EntityFilters;

public class AuthorFilter : FilterBase<Author>
{
    protected override void SetUpSpecialLambdaExpressions()
    {
    }

    public string? CONTAINS_Name { get; set; }
    public string? CONTAINS_Biography { get; set; }
}
