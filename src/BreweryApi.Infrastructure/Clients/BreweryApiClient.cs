using System.Text.Json;
using BreweryApi.Application.Abstractions;
using BreweryApi.Domain.Entities;
using BreweryApi.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace BreweryApi.Infrastructure.Clients;

public class BreweryApiClient : IApiClient
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private readonly HttpClient _client;
    private readonly IOptions<BreweryApiConfiguration> _config;

    public BreweryApiClient(HttpClient client, IOptions<BreweryApiConfiguration> config)
    {
        _client = client;
        _config = config;
    }

    public async Task<Brewery?> GetBreweryById(Guid id)
    {
        var path = $"/{id}";

        var response = await ExecuteGetRequestAsync(path);
        if (!response.IsSuccessStatusCode) return null;

        var responseJson = await response.Content.ReadAsStringAsync();
        var brewery = JsonSerializer.Deserialize<Brewery>(responseJson, _jsonSerializerOptions);

        return brewery;
    }

    public async Task<IEnumerable<Brewery>?> GetBreweries()
    {
        var path = $"?per_page={_config.Value.PerPage}";

        var response = await ExecuteGetRequestAsync(path);
        if (!response.IsSuccessStatusCode) return null;
    
        var responseJson = await response.Content.ReadAsStringAsync();
        var brewery = JsonSerializer.Deserialize<IEnumerable<Brewery>>(responseJson, _jsonSerializerOptions);

        return brewery;
    }

    private async Task<HttpResponseMessage> ExecuteGetRequestAsync(string path)
    {
        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_config.Value.Uri}{path}");

        try
        {
            return await _client.SendAsync(httpRequest);
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Request to {path} failed.", ex);
        }
    }
}