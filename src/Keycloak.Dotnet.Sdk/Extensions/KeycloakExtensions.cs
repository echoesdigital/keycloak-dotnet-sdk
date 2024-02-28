namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Extensions;

public static class KeycloakExtensions
{
    private static readonly string KeycloakApiUrl = new(Environment.GetEnvironmentVariable("AUTH_MANAGEMENT_URL")!);
    private static readonly string KeycloakApiClientId = new(Environment.GetEnvironmentVariable("AUTH_MANAGEMENT_CLIENT_ID")!);
    private static readonly string KeycloakApiUsername = new(Environment.GetEnvironmentVariable("AUTH_MANAGEMENT_USERNAME")!);
    private static readonly string KeycloakApiPassword = new(Environment.GetEnvironmentVariable("AUTH_MANAGEMENT_PASSWORD")!);

    public static KeycloakAuthConfig WithCurrentValues(this KeycloakAuthConfig config, string realm)
    {
        config.Realm = realm;
        config.ServiceUrl = KeycloakApiUrl;
        config.UserAccount = new KeycloakAuthConfig.UserAccountComposition()
        {
            ClientId = KeycloakApiClientId,
            Username = KeycloakApiUsername,
            Password = KeycloakApiPassword
        };

        return config;
    }
}
