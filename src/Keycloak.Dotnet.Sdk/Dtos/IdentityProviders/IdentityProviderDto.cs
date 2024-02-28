namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.IdentityProviders;

public record IdentityProviderDto
{
    [JsonPropertyName("alias")]
    public string? Alias { get; init; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; init; }

    [JsonPropertyName("internalId")]
    public string? InternalId { get; init; }

    [JsonPropertyName("providerId")]
    public string? ProviderId { get; init; }

    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [JsonPropertyName("updateProfileFirstLoginMode")]
    public string? UpdateProfileFirstLoginMode { get; init; }

    [JsonPropertyName("trustEmail")]
    public bool? TrustEmail { get; init; }

    [JsonPropertyName("storeToken")]
    public bool? StoreToken { get; init; }

    [JsonPropertyName("addReadTokenRoleOnCreate")]
    public bool? AddReadTokenRoleOnCreate { get; init; }

    [JsonPropertyName("authenticateByDefault")]
    public bool? AuthenticateByDefault { get; init; }

    [JsonPropertyName("linkOnly")]
    public bool? LinkOnly { get; init; }

    [JsonPropertyName("firstBrokerLoginFlowAlias")]
    public string? FirstBrokerLoginFlowAlias { get; init; }

    [JsonPropertyName("postBrokerLoginFlowAlias")]
    public string? PostBrokerLoginFlowAlias { get; init; }

    [JsonPropertyName("config")]
    public Dictionary<string, string>? Config { get; init; }

    [JsonPropertyName("updateProfileFirstLogin")]
    public bool? UpdateProfileFirstLogin { get; init; }
}