using BreweryApi.Application.Abstractions;
using BreweryApi.Domain.Models;

namespace BreweryApi.Infrastructure.Clients;

public class BreweryApiClient : IApiClient
{
    private List<Brewery> _breweries;

    public BreweryApiClient()
    {
        _breweries = new List<Brewery>
        {
            new Brewery { Id = Guid.Parse("5128df48-79fc-4f0f-8b52-d06be54d0cec"), Name = "(405) Brewing Co", City = "Norman", State = "Oklahoma", WebsiteUrl = "http://www.405brewing.com" },
            new Brewery { Id = Guid.Parse("9c5a66c8-cc13-416f-a5d9-0a769c87d318"), Name = "(512) Brewing Co", City = "Austin", State = "Texas", WebsiteUrl = "http://www.512brewing.com" },
            new Brewery { Id = Guid.Parse("34e8c68b-6146-453f-a4b9-1f6cd99a5ada"), Name = "1 of Us Brewing Company", City = "Mount Pleasant", State = "Wisconsin", WebsiteUrl = "https://www.1ofusbrewing.com" },
            new Brewery { Id = Guid.Parse("08f78223-24f8-4b71-b381-ea19a5bd82df"), Name = "11 Below Brewing Company", City = "Houston", State = "Texas", WebsiteUrl = "http://www.11belowbrewing.com" },
        };
    }

    public IEnumerable<Brewery> GetAllBreweries()
    {
        return _breweries;
    }

    public Brewery GetBreweryById(Guid breweryId)
    {
        return _breweries.FirstOrDefault(b => b.Id == breweryId);
    }

    public Brewery CreateBrewery(Brewery brewery)
    {
        brewery.Id = Guid.NewGuid();
        _breweries.Add(brewery);

        return GetBreweryById(brewery.Id);
    }

    public Brewery UpdateBrewery(Guid id, Brewery brewery)
    {
        var breweryToUpdate = GetBreweryById(id);
        if(breweryToUpdate == null)
            return null;

        breweryToUpdate.Name = brewery.Name ?? breweryToUpdate.Name;
        breweryToUpdate.City = brewery.City ?? breweryToUpdate.City;
        breweryToUpdate.State = brewery.State ?? breweryToUpdate.State;
        breweryToUpdate.WebsiteUrl = brewery.WebsiteUrl ?? breweryToUpdate.WebsiteUrl;

        return breweryToUpdate;
    }

    public IEnumerable<Brewery> DeleteBrewery(Guid id)
    {
        var breweryToRemove = GetBreweryById(id);
        if (breweryToRemove == null)
            return null;

        _breweries.Remove(breweryToRemove);

        return _breweries;
    }
}