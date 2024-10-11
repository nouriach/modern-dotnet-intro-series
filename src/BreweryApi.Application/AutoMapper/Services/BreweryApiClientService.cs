using AutoMapper;
using BreweryApi.Application.Abstractions;
using BreweryApi.Application.AutoMapper.DTOs;

namespace BreweryApi.Application.AutoMapper.Services;

public class BreweryApiClientService : IApiClientService
{
    private readonly IApiClient _apiClient;
    private readonly IMapper _mapper;

    public BreweryApiClientService(IApiClient apiClient, IMapper mapper)
    {
        _apiClient = apiClient;
        _mapper = mapper;
    }

    public async Task<BreweryDto?> GetBreweryById(Guid id)
    {
        var breweryEntity = await _apiClient.GetBreweryById(id);
        return _mapper.Map<BreweryDto>(breweryEntity);
    }

    public async Task<IEnumerable<BreweryDto>?> GetBreweries()
    {
        var breweryEntities = await _apiClient.GetBreweries();
        return _mapper.Map<IEnumerable<BreweryDto>>(breweryEntities);
    }
}