using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.DependencyInjection;

public static class DALDependencyInjection
{
    private static string _appSettingsKeyName = "DatabaseType";

    public static void RegisterDALDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        string dbType = configuration.GetSection(_appSettingsKeyName).Value ?? string.Empty;
        DatabaseTypeContext databaseTypeContext = new DatabaseTypeContext(services, configuration, dbType);
        databaseTypeContext.addDbContext();
    }
}
