using System.Linq.Expressions;
using Infrastructure.Exceptions;
using Infrastructure.Query.Filters.ExpressionStrategy;
using Infrastructure.Query.Filters.LambdaAndOr;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Query.Filters;

public abstract class FilterBase<TEntity> : IFilter<TEntity> where TEntity : class
{
    private const string _lambdaParam = "source";
    private const char _separatorCharacter = '_';
    private const string _defaultOperation = "EQ";

    protected IDictionary<string, Expression<Func<TEntity, bool>>> _lambdaDictionary;
    
    public FilterBase()
    {
        _lambdaDictionary = new Dictionary<string, Expression<Func<TEntity, bool>>>();
        SetUpSpecialLambdaExpressions();
    }

    protected abstract void SetUpSpecialLambdaExpressions();

    protected virtual Expression BuildExpression(string op, Expression left, Expression right)
    {
        ExpressionContext expressionContext = new ExpressionContext(op.IsNullOrEmpty() ? _defaultOperation : op.ToUpper());
        return expressionContext.BuildExpression(left, right);
    }

    public virtual Expression<Func<TEntity, bool>>? CreateExpression()
    {
        Expression<Func<TEntity, bool>>? final = null;
        var param = Expression.Parameter(typeof(TEntity), _lambdaParam);

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
                var split = itemName.Split(_separatorCharacter);
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
