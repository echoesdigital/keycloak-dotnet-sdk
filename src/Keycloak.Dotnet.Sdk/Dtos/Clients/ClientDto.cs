namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Clients;

public record ClientDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("rootUrl")]
    public string? RootUrl { get; set; }

    [JsonPropertyName("adminUrl")]
    public string? AdminUrl { get; set; }

    [JsonPropertyName("baseUrl")]
    public string? BaseUrl { get; set; }

    [JsonPropertyName("redirectUris")]
    public List<string>? RedirectUris { get; set; }

    [JsonPropertyName("webOrigins")]
    public List<string>? WebOrigins { get; set; }

    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    [JsonPropertyName("attributes")]
    public Dictionary<string, string>? Attributes { get; set; }
}
