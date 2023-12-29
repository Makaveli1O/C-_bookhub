namespace DataAccessLayer.DependencyInjection;

public class UnsupportedDatabaseTypeException : Exception
{
    public UnsupportedDatabaseTypeException(string dbType) : base($"Database Type <<{dbType}>> is not supported and could not be initialized!")
    {
    }
}
