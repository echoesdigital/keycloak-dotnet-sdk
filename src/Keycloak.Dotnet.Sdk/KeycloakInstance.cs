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
    }

    public RealmsResource Realms { get; }
    public ClientsResource Clients { get; }
    public UsersResource Users { get; }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}
