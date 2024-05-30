using BreweryApi.Application.Abstractions;
using BreweryApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BreweryApi.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IBreweryService, BreweryService>();
        return services;
    }
}