namespace Echoes.Digital.Keycloak.Dotnet.Sdk;

public static class Keycloak
{
    public static async Task<KeycloakInstance> Authenticate(KeycloakAuthConfig? config)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(config.ServiceUrl);
        ArgumentNullException.ThrowIfNull(config.Realm);

        var httpClient = new HttpClient();

        httpClient.BaseAddress = new Uri(config.ServiceUrl);

        var token = config.ServiceAccount != null ? await httpClient.GetServiceAccountToken(config) : await httpClient.GetUserAccountToken(config);

        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        return new KeycloakInstance(httpClient);
    }

    private static async Task<string> GetServiceAccountToken(this HttpClient httpClient,
        KeycloakAuthConfig config)
    {
        ArgumentNullException.ThrowIfNull(config.ServiceAccount?.ClientId);
        ArgumentNullException.ThrowIfNull(config.ServiceAccount?.ClientSecret);

        var credentials = Convert.ToBase64String(Encoding
            .GetEncoding("ISO-8859-1")
            .GetBytes($"{config.ServiceAccount.ClientId}:{config.ServiceAccount.ClientSecret}"));

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {credentials}");

        var formEncoded = new List<KeyValuePair<string, string>>
        {
            new ("grant_type", "client_credentials")
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, $"/realms/{config.Realm}/protocol/openid-connect/token")
        {
            Content = new FormUrlEncodedContent(formEncoded)
        };

        using var response = await httpClient.SendAsync(loginRequest).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new KeycloakException("Could not authenticate with the provided data");

        var token = await response.Content.ReadAsJsonAsync<TokenResponseDto>();

        return token!.AccessToken!;
    }

    private static async Task<string> GetUserAccountToken(this HttpClient httpClient,
        KeycloakAuthConfig config)
    {
        ArgumentNullException.ThrowIfNull(config.UserAccount?.ClientId);
        ArgumentNullException.ThrowIfNull(config.UserAccount?.Username);
        ArgumentNullException.ThrowIfNull(config.UserAccount?.Password);

        var formEncoded = new List<KeyValuePair<string, string>>
        {
            new ("client_id", config.UserAccount.ClientId),
            new ("username", config.UserAccount.Username),
            new ("password", config.UserAccount.Password),
            new ("grant_type", "password")
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, $"/realms/{config.Realm}/protocol/openid-connect/token")
        {
            Content = new FormUrlEncodedContent(formEncoded)
        };

        using var response = await httpClient.SendAsync(loginRequest).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new KeycloakException("Could not authenticate with the provided data");

        var token = await response.Content.ReadAsJsonAsync<TokenResponseDto>();

        return token!.AccessToken!;
    }
}