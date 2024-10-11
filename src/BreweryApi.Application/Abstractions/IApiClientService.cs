using BreweryApi.Application.AutoMapper.DTOs;

namespace BreweryApi.Application.Abstractions;

public interface IApiClientService
{
    Task<BreweryDto?> GetBreweryById(Guid id);
    Task<IEnumerable<BreweryDto>?> GetBreweries();
}