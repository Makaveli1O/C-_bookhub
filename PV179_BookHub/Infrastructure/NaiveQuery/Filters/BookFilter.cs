using DataAccessLayer.Models.Enums;
using DataAccessLayer.Models.Publication;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.NaiveQuery.Filters;

public class BookFilter : IFilter<Book>
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

    // https://stackoverflow.com/questions/34120561/linq-with-reflection-for-nested-property-values
    public static BinaryExpression PropertyEquals<TItem, TValue>(ParameterExpression param, string propertyPath, TValue value)
    {
        var propertyNames = propertyPath.Split('.');
        var member = Expression.Property(param, propertyNames[0]);
        for (int i = 1; i < propertyNames.Length; i++)
            member = Expression.Property(member, propertyNames[i]);
        Expression left = member, right = Expression.Constant(value, typeof(TValue));
        if (left.Type != right.Type)
        {
            var nullableType = Nullable.GetUnderlyingType(left.Type);
            if (nullableType != null)
                right = Expression.Convert(right, left.Type);
            else
                left = Expression.Convert(left, right.Type);
        }
        var body = Expression.Equal(left, right);
        return body;
    }

    public Expression<Func<Book, bool>> CreateExpression()
    {
        var param = Expression.Parameter(typeof(Book), "book");
        Expression? body = null;
        foreach (var item in GetType().GetProperties())
        {
            var constant = Expression.Constant(item.GetValue(this));
            if (constant.Value == null)
            {
                continue;
            }

            MemberExpression member;
            Expression expression;

            switch(item.Name)
            {
                case "Author":
                    PropertyInfo authorsProp = typeof(Book).GetProperty("Authors");
                    ParameterExpression authorParam = Expression.Parameter(typeof(Author), "author");
                    expression =
                        Expression.Call(
                            typeof(Enumerable),
                            "Any",
                            new[] { typeof(Author) },
                            Expression.MakeMemberAccess(param, authorsProp),
                            Expression.Lambda<Func<Author, bool>>(
                                Expression.Equal(
                                    Expression.Property(authorParam, "Name"),
                                    Expression.Constant(Author)
                                    ),
                                authorParam
                                )
                            );
                    break;
                case "Publisher":
                    expression = PropertyEquals<Book, string>(param, "Publisher.Name", Publisher);
                    break;
                default:
                    member = Expression.Property(param, item.Name);
                    expression = Expression.Equal(member, constant);
                    break;
            }

            body = body == null ? expression : Expression.AndAlso(body, expression);
            // Where(book => book.Authors.Any(a => a.Name == Authors.Name))
        }
        return Expression.Lambda<Func<Book, bool>>(body, param);
    }
}
