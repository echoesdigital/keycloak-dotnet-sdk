namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.AuthenticationFlows;

public record AddExecutionRequestDto
{
    [JsonPropertyName("provider")]
    public string? Provider { get; set; }
}
