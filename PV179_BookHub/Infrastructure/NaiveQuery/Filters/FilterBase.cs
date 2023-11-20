using System.Linq.Expressions;
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
                MemberExpression member = Expression.Property(param, item.Name);
                Expression expression = Expression.Equal(member, constant);
                current = Expression.Lambda<Func<TEntity, bool>>(expression, param);
            }

            if (final == null)
            {
                final = current;
            }
            else
            {
                final = final.And(current);
            }

        }
        return final;
    }
}
