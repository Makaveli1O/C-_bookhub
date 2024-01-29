namespace Infrastructure.Exceptions;

public class XmlResponseConverterMiddlewareException : Exception
{
    public XmlResponseConverterMiddlewareException(string message)
        : base($"XmlResponseConverterMiddlewareExcaption : {message}")
    {
    }
}
