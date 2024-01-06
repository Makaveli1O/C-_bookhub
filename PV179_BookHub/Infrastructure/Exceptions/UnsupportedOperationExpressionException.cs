namespace Infrastructure.Exceptions;

public class UnsupportedOperationExpressionException : Exception
{
    public UnsupportedOperationExpressionException(string opName) : base($"Operation <<{opName}>> is not supported!") { }
}
