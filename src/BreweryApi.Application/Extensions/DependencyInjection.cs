using BreweryApi.Application.Abstractions;
using BreweryApi.Application.AutoMapper.Profiles;
using BreweryApi.Application.AutoMapper.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BreweryApi.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(typeof(BreweryProfile).Assembly);
        services.AddScoped<IApiClientService, BreweryApiClientService>();
        return services;
    }
}