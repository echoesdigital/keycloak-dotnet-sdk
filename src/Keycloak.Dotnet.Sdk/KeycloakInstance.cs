namespace Echoes.Digital.Keycloak.Dotnet.Sdk;

public class KeycloakInstance : IDisposable
{
    public readonly HttpClient HttpClient;

    public KeycloakInstance(HttpClient httpClient)
    {
        HttpClient = httpClient;
        Realms = new RealmsResource(this);
        Clients = new ClientsResource(this);
    }

    public RealmsResource Realms { get; }
    public ClientsResource Clients { get; }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}
