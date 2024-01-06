using System.Linq.Expressions;

namespace Infrastructure.NaiveQuery.Filters.ExpressionStrategy.ExpressionOperation;

public interface IExpressionOperation
{
    Expression BuildExpression(Expression left, Expression right);
}
