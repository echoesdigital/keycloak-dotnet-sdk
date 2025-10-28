namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.AuthenticationFlows;

public record AuthenticationFlowDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("alias")]
    public string? Alias { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("providerId")]
    public string? ProviderId { get; set; }

    [JsonPropertyName("topLevel")]
    public bool? TopLevel { get; set; }

    [JsonPropertyName("builtIn")]
    public bool? BuiltIn { get; set; }

    [JsonPropertyName("authenticationExecutions")]
    public List<AuthenticationExecutionExportRepresentation>? AuthenticationExecutions { get; set; }
}
