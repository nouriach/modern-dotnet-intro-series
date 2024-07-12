using BreweryApi.Application.Features.Interfaces.Commands;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Features.Breweries.Commands.CreateBrewery;

public class CreateBrewery : ICommand<BreweryResponse>
{
    public string Name { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string WebsiteUrl { get; set; }
}