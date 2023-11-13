using DataAccessLayer.Models.Enums;
using System.Linq.Expressions;

namespace BookHubWebAPI.Api.Book.Filter;


// THIS CLASS IS ONLY TEMPORARY, it will be surely rebuild in the next milestone
public class BookFilter
{
    public BookFilter(IDictionary<string, string> filters)
    {
        foreach (var property in GetType().GetProperties())
        {
            var propertyName = property.Name.ToLower();
            if (!filters.ContainsKey(propertyName))
            {
                continue;
            }

            if (property.PropertyType == typeof(double?))
            {
                property.SetValue(this, Convert.ToDouble(filters[propertyName]));
            }
            else if (property.PropertyType == typeof(BookGenre?))
            {
                property.SetValue(this, Enum.Parse<BookGenre>(filters[propertyName]));
            }
            else
            {
                property.SetValue(this, filters[propertyName]);
            }
        }
    }

    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public BookGenre? BookGenre { get; set; }
    public double? Price { get; set; }


    // source: https://code-maze.com/dynamic-queries-expression-trees-csharp/
    public Expression<Func<DataAccessLayer.Models.Book, bool>> CreateEqualExpression()
    {
        var param = Expression.Parameter(typeof(DataAccessLayer.Models.Book), "book");
        Expression? body = null;
        foreach (var item in GetType().GetProperties())
        {
            var member = Expression.Property(param, item.Name);
            var constant = Expression.Constant(item.GetValue(this));
            if (constant.Value == null)
            {
                continue;
            }
            var expression = Expression.Equal(member, constant);
            body = body == null ? expression : Expression.AndAlso(body, expression);
        }
        return Expression.Lambda<Func<DataAccessLayer.Models.Book, bool>>(body, param);
    }
}
