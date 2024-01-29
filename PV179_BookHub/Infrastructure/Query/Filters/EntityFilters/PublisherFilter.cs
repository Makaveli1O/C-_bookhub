using DataAccessLayer.Models.Publication;

namespace Infrastructure.Query.Filters.EntityFilters;

public class PublisherFilter : FilterBase<Publisher>
{
    protected override void SetUpSpecialLambdaExpressions()
    {
    }

    public string? CONTAINS_Name { get; set; }
    public string? CONTAINS_City { get; set; }
    public string? CONTAINS_Country { get; set; }
    public ushort? GE_YearFounded { get; set; }
    public ushort? LE_YearFounded { get; set; }
}
