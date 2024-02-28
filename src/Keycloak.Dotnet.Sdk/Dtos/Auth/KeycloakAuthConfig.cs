namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Auth;

public record KeycloakAuthConfig
{
    public string? ServiceUrl { get; set; }
    public string? Realm { get; set; }
    public UserAccountComposition? UserAccount { get; set; }
    public ServiceAccountComposition? ServiceAccount { get; set; }

    public record UserAccountComposition
    {
        public string? ClientId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public record ServiceAccountComposition
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
