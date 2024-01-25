using DataAccessLayer.Models.Enums;
using DataAccessLayer.Models.Publication;

namespace Infrastructure.Query.Filters.EntityFilters;

public class BookFilter : FilterBase<Book>
{
    public BookFilter() : base()
    {
    }

    protected override void SetUpSpecialLambdaExpressions()
    {
        _lambdaDictionary.Add("Author", source => source.Authors.Any(author => author.Name.Contains(Author)));
        _lambdaDictionary.Add("Publisher", source => source.Publisher.Name.Contains(Publisher));
    }

    public string? CONTAINS_Title { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public string? CONTAINS_Description { get; set; }
    public string? CONTAINS_ISBN { get; set; }
    public BookGenre? BookGenre { get; set; }
    public double? LE_Price { get; set; }
    public double? GE_Price { get; set; }
}
