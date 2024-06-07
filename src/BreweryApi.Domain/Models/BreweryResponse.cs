using BreweryApi.Domain.Entities;

namespace BreweryApi.Domain.Models;

public class BreweryResponse
{
    public BreweryResponse(Brewery brewery)
    {
        Id = brewery.Id;
        Name = brewery.Name;
        City = brewery.City;
        State = brewery.State;
        WebsiteUrl = brewery.WebsiteUrl;
    }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string WebsiteUrl { get; private set; }
}