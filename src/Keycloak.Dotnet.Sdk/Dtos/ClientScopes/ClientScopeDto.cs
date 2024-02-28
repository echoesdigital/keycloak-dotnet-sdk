namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.ClientScopes;

public record ClientScopeDto
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("protocol")]
    public string? Protocol { get; init; }

    [JsonPropertyName("attributes")]
    public Dictionary<string, string>? Attributes { get; init; }
}