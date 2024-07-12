using BreweryApi.Application.Features.Interfaces.Queries;
using BreweryApi.Domain.Models;

namespace BreweryApi.Application.Features.Breweries.Queries.GetBreweryById;

public class GetBreweryById : IQuery<BreweryResponse?>
{
    public Guid Id { get; set; }
}