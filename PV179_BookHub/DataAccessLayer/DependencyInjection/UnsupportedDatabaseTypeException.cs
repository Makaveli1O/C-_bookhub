using DataAccessLayer.DependencyInjection.DBStrategies;

namespace DataAccessLayer.DependencyInjection;

public class UnsupportedDatabaseTypeException : Exception
{
    public UnsupportedDatabaseTypeException(DatabaseType dbType) : base($"Database Type <<{dbType}>> is not supported and could not be initialized!")
    {
    }
}
