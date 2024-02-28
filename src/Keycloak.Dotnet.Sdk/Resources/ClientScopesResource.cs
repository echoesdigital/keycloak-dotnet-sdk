namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Resources;

public class ClientScopesResource(KeycloakInstance instance)
{
    public async Task<ResultResponseDto> Create(string realm, ClientScopeDto clientScope)
    {
        var uri = new Uri($"admin/realms/{realm}/client-scopes", UriKind.Relative);

        using var response = await instance.HttpClient.PostJsonAsync(uri, clientScope).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<ClientScopeDto>> Get(string realm, string clientScopeUuid)
    {
        var uri = new Uri($"admin/realms/{realm}/client-scopes/{clientScopeUuid}", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<ClientScopeDto>
        {
            Data = await response.Content.ReadAsJsonAsync<ClientScopeDto>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<List<ClientScopeDto>>> List(string realm)
    {
        var uri = new Uri($"admin/realms/{realm}/client-scopes", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<List<ClientScopeDto>>
        {
            Data = await response.Content.ReadAsJsonAsync<List<ClientScopeDto>>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Update(string realm, ClientScopeDto clientScope)
    {
        var uri = new Uri($"admin/realms/{realm}/client-scopes/{clientScope.Id}", UriKind.Relative);

        using var response = await instance.HttpClient.PutJsonAsync(uri, clientScope).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Delete(string realm, string clientScopeUuid)
    {
        var uri = new Uri($"admin/realms/{realm}/client-scopes/{clientScopeUuid}", UriKind.Relative);

        using var response = await instance.HttpClient.DeleteAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }
}