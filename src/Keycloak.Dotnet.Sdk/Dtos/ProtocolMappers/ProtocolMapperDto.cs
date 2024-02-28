namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.ProtocolMappers;

public record ProtocolMapperDto
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("protocol")]
    public string? Protocol { get; init; }

    [JsonPropertyName("protocolMapper")]
    public string? ProtocolMapper { get; init; }

    [JsonPropertyName("consentRequired")]
    public bool? ConsentRequired { get; init; }

    [JsonPropertyName("consentText")]
    public string? ConsentText { get; init; }

    [JsonPropertyName("config")]
    public Dictionary<string, string>? Config { get; init; }
}