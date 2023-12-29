using DataAccessLayer.DependencyInjection.DBStrategies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.DependencyInjection;

public class DatabaseTypeContext
{
    public IDBStrategy DatabaseStrategy { get; set; }
    public IServiceCollection Services { get; set; }
    public IConfiguration Config { get; set; }

    public DatabaseTypeContext(IServiceCollection services, IConfiguration configuration, string dbType)
    {
        Services = services;
        Config = configuration;
        setDatabaseStrategy(dbType);
    }

    private void setDatabaseStrategy(string dbType)
    {
        switch (dbType)
        {
            case "SQLite":
                DatabaseStrategy = new SQLiteDbStrategy(Config);
                break;
            case "MSSQL":
                DatabaseStrategy = new MSSQLDbStrategy(Config);
                break;
            default:
                throw new Exception();
        }
    }

    public void addDbContext()
    {
        Services = DatabaseStrategy.addDbContext(Services);
    }
}
