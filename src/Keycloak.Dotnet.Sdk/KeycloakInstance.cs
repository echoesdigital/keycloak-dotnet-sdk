namespace Echoes.Digital.Keycloak.Dotnet.Sdk;

public sealed class KeycloakInstance : IDisposable
{
    public readonly HttpClient HttpClient;

    private bool _disposed = false;

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
        UsersProfileResource = new UsersProfileResource(this);
        AuthenticationFlows = new AuthenticationFlowsResource(this);
    }

    public RealmsResource Realms { get; }
    public ClientsResource Clients { get; }
    public UsersResource Users { get; }
    public RolesResource Roles { get; }
    public RoleMappingsResource RoleMappings { get; }
    public ClientScopesResource ClientScopes { get; }
    public IdentityProvidersResource IdentityProviders { get; }
    public ProtocolMappersResource ProtocolMappers { get; }
    public UsersProfileResource UsersProfileResource { get; }
    public AuthenticationFlowsResource AuthenticationFlows { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing) HttpClient.Dispose();
        _disposed = true;
    }

    ~KeycloakInstance()
    {
        Dispose(false);
    }
}
