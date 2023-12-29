using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.DependencyInjection.DBStrategies;

public class MSSQLDbStrategy : IDBStrategy
{
    private static string _migrationsProject = "DAL.MSSql.Migrations";

    public MSSQLDbStrategy(IConfiguration configuration) : base(configuration)
    {
    }

    public override IServiceCollection addDbContext(IServiceCollection services)
    {
        services.AddDbContextFactory<BookHubDbContext>(options =>
        {
            options
                .UseSqlServer(
                    Config.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly(_migrationsProject)
                )
                .UseLazyLoadingProxies();
        });

        return services;
    }
}
