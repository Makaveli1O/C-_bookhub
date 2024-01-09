namespace Infrastructure.Exceptions;

public class UnsupportedPropertyNameException : Exception
{
    public UnsupportedPropertyNameException(string name) : base($"Query Filter property with name {name} is not supported or is not instantiated correctly!")
    {
    }
}
