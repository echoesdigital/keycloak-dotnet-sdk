namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.IdentityProviders;

public record IdentityProviderMapperDto
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("identityProviderAlias")]
    public string? IdentityProviderAlias { get; init; }

    [JsonPropertyName("identityProviderMapper")]
    public string? IdentityProviderMapper { get; init; }

    [JsonPropertyName("config")]
    public ConfigComposition? Config { get; init; }

    public record ConfigComposition
    {
        [JsonPropertyName("syncMode")]
        public string? SyncMode { get; init; }

        [JsonPropertyName("claim")]
        public string? Claim { get; init; }

        [JsonPropertyName("are.claim.values.regex")]
        public string? AreClaimValuesRegex { get; init; }

        [JsonPropertyName("role")]
        public string? Role { get; init; }

        [JsonPropertyName("user.attribute")]
        public string? UserAttribute { get; init; }

        [JsonPropertyName("attribute")]
        public string? Attribute { get; init; }

        [JsonPropertyName("claims")]
        public string? Claims { get; init; }
    }
}