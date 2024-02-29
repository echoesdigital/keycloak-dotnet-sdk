namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.IdentityProviders;

public record IdentityProviderImportConfigDto
{
    [JsonPropertyName("fromUrl")]
    public string? FromUrl { get; init; }

    [JsonPropertyName("providerId")]
    public string? ProviderId { get; init; }
}