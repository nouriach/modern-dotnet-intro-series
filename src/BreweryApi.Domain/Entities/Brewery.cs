using System.Text.Json.Serialization;

namespace BreweryApi.Domain.Entities;

public class Brewery
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    [JsonPropertyName("website_url")]
    public string WebsiteUrl { get; set; }
}