namespace Echoes.Digital.Keycloak.Dotnet.Sdk;

public class KeycloakInstance : IDisposable
{
    public readonly HttpClient HttpClient;

    public KeycloakInstance(HttpClient httpClient)
    {
        HttpClient = httpClient;
        Realms = new RealmsResource(this);
        Clients = new ClientsResource(this);
        Users = new UsersResource(this);
        Roles = new RolesResource(this);
        UserRoleMappings = new UserRoleMappingsResource(this);
        ClientScopes = new ClientScopesResource(this);
        IdentityProviders = new IdentityProvidersResource(this);
    }

    public RealmsResource Realms { get; }
    public ClientsResource Clients { get; }
    public UsersResource Users { get; }
    public RolesResource Roles { get; }
    public UserRoleMappingsResource UserRoleMappings { get; }
    public ClientScopesResource ClientScopes { get; }
    public IdentityProvidersResource IdentityProviders { get; }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}
