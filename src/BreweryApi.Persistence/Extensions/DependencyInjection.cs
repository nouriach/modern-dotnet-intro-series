using BreweryApi.Application.Abstractions;
using BreweryApi.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BreweryApi.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IBreweryRepository, BreweryRepository>();
        return services;
    }
}