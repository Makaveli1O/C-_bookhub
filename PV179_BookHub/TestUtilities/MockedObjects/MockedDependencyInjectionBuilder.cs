using Microsoft.Extensions.DependencyInjection;
using Infrastructure.DependencyInjection;
using BusinessLayer.DependencyInjection;

namespace TestUtilities.MockedObjects;

public class MockedDependencyInjectionBuilder
{
    private IServiceCollection _serviceCollection = new ServiceCollection();

    public MockedDependencyInjectionBuilder AddInfrastructure()
    {
        _serviceCollection.RegisterInfrastructureDependencies();

        return this;
    }

    public MockedDependencyInjectionBuilder AddBusinessLayer()
    {
        _serviceCollection.RegisterBLDependencies();

        return this;
    }

    public MockedDependencyInjectionBuilder AddScoped<T>(T objectToRegister)
        where T : class
    {
        _serviceCollection = _serviceCollection
            .AddScoped<T>(_ => objectToRegister);

        return this;
    }

    public ServiceProvider Create()
    {
        return _serviceCollection.BuildServiceProvider();
    }
}
