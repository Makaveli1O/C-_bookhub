using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.DependencyInjection.DBStrategies;

public abstract class IDBStrategy
{
    public readonly IConfiguration Config;

    public IDBStrategy(IConfiguration configuration)
    {
        Config = configuration;
    }

    public abstract IServiceCollection addDbContext(IServiceCollection services);
}
