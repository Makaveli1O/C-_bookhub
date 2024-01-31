using MVC.Mappers;

namespace MVC.DependencyInjection;

public static class MVCDependencyInjection
{
    public static void RegiterMVCDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(FiltersProfile));
        services.AddAutoMapper(typeof(MvcDtoProfile));
    }
}
