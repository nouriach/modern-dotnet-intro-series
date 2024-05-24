using BreweryApi.Application.Abstractions;
using BreweryApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BreweryApi.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IBreweryRepository, BreweryRepository>();
        return services;
    }
}