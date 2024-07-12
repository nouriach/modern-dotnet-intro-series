using BreweryApi.Application.Features.Interfaces.Commands;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Features.Breweries.Commands.UpdateBrewery;

public class UpdateBreweryRequest : ICommand<BreweryResponse>
{
    public Guid Id { get; set; }
    public UpdateBrewery Command { get; set; }
}