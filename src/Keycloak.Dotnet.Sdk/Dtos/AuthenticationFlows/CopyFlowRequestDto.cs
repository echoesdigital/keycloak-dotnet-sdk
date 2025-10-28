namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.AuthenticationFlows;

public record CopyFlowRequestDto
{
    [JsonPropertyName("newName")]
    public string? NewName { get; set; }
}
