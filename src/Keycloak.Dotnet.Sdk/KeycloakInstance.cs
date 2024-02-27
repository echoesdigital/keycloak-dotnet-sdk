namespace Echoes.Digital.Keycloak.Dotnet.Sdk;

public class KeycloakInstance : IDisposable
{
    public readonly HttpClient HttpClient;

    public KeycloakInstance(HttpClient httpClient)
    {
        HttpClient = httpClient;
        Realms = new RealmsResource(this);
    }

    public RealmsResource Realms { get; }

    public void Dispose()
    {
        HttpClient.Dispose();
    }
}
