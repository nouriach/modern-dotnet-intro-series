using BreweryApi.Application.Abstractions;
using BreweryApi.Infrastructure.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace BreweryApi.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IApiClient, BreweryApiClient>();
        return services;
    }
}