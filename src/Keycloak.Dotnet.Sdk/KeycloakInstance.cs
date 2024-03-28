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
        RoleMappings = new RoleMappingsResource(this);
        ClientScopes = new ClientScopesResource(this);
        IdentityProviders = new IdentityProvidersResource(this);
        ProtocolMappers = new ProtocolMappersResource(this);
    }

    public RealmsResource Realms { get; }
    public ClientsResource Clients { get; }
    public UsersResource Users { get; }
    public RolesResource Roles { get; }
    public RoleMappingsResource RoleMappings { get; }
    public ClientScopesResource ClientScopes { get; }
    public IdentityProvidersResource IdentityProviders { get; }
    public ProtocolMappersResource ProtocolMappers { get; }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}
