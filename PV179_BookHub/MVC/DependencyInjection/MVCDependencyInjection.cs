using MVC.Mappers;

namespace MVC.DependencyInjection;

public static class MVCDependencyInjection
{
    public static void RegiterMVCDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(FilterBookProfile));
        services.AddAutoMapper(typeof(FilterAuthorProfile));
        services.AddAutoMapper(typeof(FilterPublisherProfile));
        services.AddAutoMapper(typeof(MvcDtoProfile));
    }
}
