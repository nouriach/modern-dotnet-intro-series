using BreweryApi.Application.Abstractions;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Services;

public class BreweryService : IBreweryService
{
    private IApiClient _breweryClient;
    public BreweryService(IApiClient breweryClient)
    {
        _breweryClient = breweryClient;
    }
    public IEnumerable<Brewery> GetAllBreweries()
    {
        return _breweryClient.GetAllBreweries();
    }

    public Brewery  GetBreweryById(Guid breweryId)
    {
        return _breweryClient.GetBreweryById(breweryId);
    }

    public Brewery  CreateBrewery(Brewery brewery)
    {
        return _breweryClient.CreateBrewery(brewery);
    }

    public Brewery UpdateBrewery(Guid id, Brewery brewery)
    {
        return _breweryClient.UpdateBrewery(id, brewery);
    }

    public IEnumerable<Brewery> DeleteBrewery(Guid id)
    {
        return _breweryClient.DeleteBrewery(id);
    }
}