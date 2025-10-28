namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.AuthenticationFlows;

public record AuthenticatorConfigRepresentation
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("alias")]
    public string? Alias { get; set; }

    [JsonPropertyName("config")]
    public Dictionary<string, string>? Config { get; set; }
}
