namespace BreweryApi.Domain.Models;

public class BreweryUpsertRequest
{
    public string Name { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string WebsiteUrl { get; set; }
}