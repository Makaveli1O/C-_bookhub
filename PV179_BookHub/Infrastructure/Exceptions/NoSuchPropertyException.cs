
namespace Infrastructure.Exceptions;

public class NoSuchPropertyException : Exception
{
    public NoSuchPropertyException(string prop) : base($"Property with Name <<{prop}>> does not exist!")
    {
    }
}
