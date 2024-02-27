namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Auth;

public record KeycloakAuthConfig
{
    public string? ServiceUrl { get; set; }
    public string? Realm { get; set; }
    public UserAccountAggregation? UserAccount { get; set; }
    public ServiceAccountAggregation? ServiceAccount { get; set; }

    public record UserAccountAggregation
    {
        public string? ClientId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public record ServiceAccountAggregation
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
