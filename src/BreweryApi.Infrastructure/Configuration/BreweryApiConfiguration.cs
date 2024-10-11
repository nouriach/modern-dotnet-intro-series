namespace BreweryApi.Infrastructure.Configuration;

public class BreweryApiConfiguration
{
    public const string ConfigSectionName = "BreweryApiConfig";
    public string Uri { get; init; } = default!;
    public int PerPage { get; init; }
}