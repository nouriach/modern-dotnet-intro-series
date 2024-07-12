using BreweryApi.Application.Features.Interfaces.Commands;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Features.Breweries.Commands.DeleteBrewery;

public class DeleteBrewery : ICommand<IEnumerable<BreweryResponse>>
{
    public Guid Id { get; set; }
}