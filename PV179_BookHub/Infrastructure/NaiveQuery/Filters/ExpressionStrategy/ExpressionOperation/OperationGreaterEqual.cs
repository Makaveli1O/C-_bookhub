using System.Linq.Expressions;

namespace Infrastructure.NaiveQuery.Filters.ExpressionStrategy.ExpressionOperation;

public class OperationGreaterEqual : IExpressionOperation
{
    public Expression BuildExpression(Expression left, Expression right)
    {
        return Expression.GreaterThanOrEqual(left, right);
    }
}
