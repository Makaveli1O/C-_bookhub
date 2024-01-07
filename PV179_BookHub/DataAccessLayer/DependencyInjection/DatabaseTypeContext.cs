using DataAccessLayer.DependencyInjection.DBStrategies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.DependencyInjection;

public class DatabaseTypeContext
{
    private static string _appSettingsKeyName = "DatabaseType";
    public IDBStrategy DatabaseStrategy { get; set; }
    public IServiceCollection Services { get; set; }
    public IConfiguration Config { get; set; }

    public DatabaseTypeContext(IServiceCollection services, IConfiguration configuration)
    {
        Services = services;
        Config = configuration;
        SetDatabaseStrategy();
    }

    private void SetDatabaseStrategy()
    {
        if (!Enum.TryParse(Config.GetSection(_appSettingsKeyName).Value ?? string.Empty, out DatabaseType dbType))
        {
            throw new UnsupportedDatabaseTypeException(dbType);
        }

        switch (dbType)
        {
            case DatabaseType.SQLite:
                DatabaseStrategy = new SQLiteDbStrategy(Config);
                break;
            case DatabaseType.MSSQL:
                DatabaseStrategy = new MSSQLDbStrategy(Config);
                break;
            default:
                throw new UnsupportedDatabaseTypeException(dbType);
        }
    }

    public void AddDbContext()
    {
        Services = DatabaseStrategy.AddDbContext(Services);
    }
}
