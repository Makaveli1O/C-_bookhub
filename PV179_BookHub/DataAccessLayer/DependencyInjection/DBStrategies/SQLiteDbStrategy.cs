using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.DependencyInjection.DBStrategies;

public class SQLiteDbStrategy : IDBStrategy
{
    private static string _migrationsProject = "DAL.SQLite.Migrations";

    public SQLiteDbStrategy(IConfiguration configuration) : base(configuration)
    {
    }

    public override IServiceCollection AddDbContext(IServiceCollection services)
    {
        services.AddDbContextFactory<BookHubDbContext>(options =>
        {
            options
                .UseSqlite(
                    Config.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly(_migrationsProject)
                    )
                .UseLazyLoadingProxies();
        });

        return services;
    }
}
