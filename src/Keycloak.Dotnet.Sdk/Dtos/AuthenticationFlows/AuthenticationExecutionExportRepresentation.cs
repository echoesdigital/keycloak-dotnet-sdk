namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.AuthenticationFlows;

public record AuthenticationExecutionExportRepresentation
{
    [JsonPropertyName("authenticatorConfig")]
    public string? AuthenticatorConfig { get; set; }

    [JsonPropertyName("authenticator")]
    public string? Authenticator { get; set; }

    [JsonPropertyName("authenticatorFlow")]
    public bool? AuthenticatorFlow { get; set; }

    [JsonPropertyName("requirement")]
    public string? Requirement { get; set; }

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }

    [JsonPropertyName("autheticatorFlow")]
    public bool? AutheticatorFlow { get; set; }

    [JsonPropertyName("flowAlias")]
    public string? FlowAlias { get; set; }

    [JsonPropertyName("userSetupAllowed")]
    public bool? UserSetupAllowed { get; set; }
}
