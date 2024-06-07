using BreweryApi.Application.Abstractions;
using BreweryApi.Domain.Entities;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Services;

public class BreweryService : IBreweryService
{
    private IBreweryRepository _breweryRepository;
    public BreweryService(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }
    public async Task<IEnumerable<BreweryResponse>> GetAllBreweries()
    {
        var breweries = await _breweryRepository.GetAllBreweries();
        return breweries.Select(brewery => new BreweryResponse(brewery));
    }

    public async Task<BreweryResponse> GetBreweryById(Guid breweryId)
    {
        var brewery = await _breweryRepository.GetBreweryById(breweryId);
        if(brewery == null)
            return null;

        return new BreweryResponse(brewery);
    }

    public async Task<BreweryResponse> CreateBrewery(BreweryUpsertRequest brewery)
    {
        var breweryEntity = new Brewery
        {
            Name = brewery.Name,
            City = brewery.City,
            State = brewery.State,
            WebsiteUrl = brewery.WebsiteUrl
        };

        var breweryToReturn = await _breweryRepository.CreateBrewery(breweryEntity);
        if(breweryToReturn == null)
            return null;

        return new BreweryResponse(breweryToReturn);
    }

    public async Task<BreweryResponse> UpdateBrewery(Guid id, BreweryUpsertRequest brewery)
    {
        var breweryEntity = new Brewery
        {
            Name = brewery.Name,
            City = brewery.City,
            State = brewery.State,
            WebsiteUrl = brewery.WebsiteUrl
        };

        var breweryToReturn = await _breweryRepository.UpdateBrewery(id, breweryEntity);
        if(brewery == null)
            return null;

        return new BreweryResponse(breweryToReturn);
    }

    public async Task<IEnumerable<BreweryResponse>> DeleteBrewery(Guid id)
    {
        var breweries = await _breweryRepository.DeleteBrewery(id);
        return breweries.Select(brewery => new BreweryResponse(brewery));
    }
}