using System.Linq.Expressions;
using Infrastructure.Exceptions;
using Infrastructure.NaiveQuery.Filters.LambdaAndOr;

namespace Infrastructure.NaiveQuery.Filters;

public abstract class FilterBase<TEntity> : IFilter<TEntity> where TEntity : class
{
    protected IDictionary<string, Expression<Func<TEntity, bool>>> _lambdaDictionary;

    public FilterBase()
    {
        _lambdaDictionary = new Dictionary<string, Expression<Func<TEntity, bool>>>();
        SetUpSpecialLambdaExpressions();
    }

    protected abstract void SetUpSpecialLambdaExpressions();

    protected virtual Expression BuildExpression(string op, Expression left, Expression right)
    {
        switch(op.ToUpper())
        {
            case "LEQ":
                return Expression.LessThanOrEqual(left, right);
            case "LE":
                return Expression.LessThan(left, right);
            case "GEQ":
                return Expression.GreaterThanOrEqual(left, right);
            case "GE":
                return Expression.GreaterThan(left, right);
            default:
                return Expression.Equal(left, right);
        }
    }

    public virtual Expression<Func<TEntity, bool>>? CreateExpression()
    {
        Expression<Func<TEntity, bool>>? final = null;
        var param = Expression.Parameter(typeof(TEntity), "source");

        foreach (var item in GetType().GetProperties())
        {
            var constant = Expression.Constant(item.GetValue(this));
            if (constant.Value == null)
            {
                continue;
            }

            Expression<Func<TEntity, bool>>? current;
            if (_lambdaDictionary.ContainsKey(item.Name))
            {
                current = _lambdaDictionary[item.Name];
            }
            else
            {
                var itemName = item.Name;
                string op = string.Empty;
                var split = itemName.Split('_');
                if (split.Length == 2)
                {
                    itemName = split[1];
                    op = split[0];
                }
                else if (split.Length > 3)
                {
                    throw new UnsupportedPropertyNameException(item.Name);
                }
                MemberExpression member = Expression.Property(param, itemName);
                Expression expression = BuildExpression(op, member, constant);
                current = Expression.Lambda<Func<TEntity, bool>>(expression, param);
            }

            final = final == null ? current : final.And(current);
        }
        return final;
    }
}
