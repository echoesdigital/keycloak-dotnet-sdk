namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Resources;

public class ProtocolMappersResource(KeycloakInstance instance)
{
    public async Task<ResultResponseDto> Create(string realm, string clientScopeUuid, ProtocolMapperDto protocolMapper)
    {
        var uri = new Uri($"admin/realms/{realm}/client-scopes/{clientScopeUuid}/protocol-mappers/models", UriKind.Relative);

        using var response = await instance.HttpClient.PostJsonAsync(uri, protocolMapper).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Get(string realm, string clientScopeUuid, string protocolMapperUuid)
    {
        var uri = new Uri($"admin/realms/{realm}/client-scopes/{clientScopeUuid}/protocol-mappers/models/{protocolMapperUuid}", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<List<ProtocolMapperDto>>> List(string realm, string clientScopeUuid)
    {
        var uri = new Uri($"admin/realms/{realm}/client-scopes/{clientScopeUuid}/protocol-mappers/models", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<List<ProtocolMapperDto>>
        {
            Data = await response.Content.ReadAsJsonAsync<List<ProtocolMapperDto>>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Update(string realm, string clientScopeUuid, ProtocolMapperDto protocolMapper)
    {
        var uri = new Uri($"admin/realms/{realm}/client-scopes/{clientScopeUuid}/protocol-mappers/models/{protocolMapper.Id}", UriKind.Relative);

        using var response = await instance.HttpClient.PutJsonAsync(uri, protocolMapper).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Delete(string realm, string clientScopeUuid, string protocolMapperUuid)
    {
        var uri = new Uri($"admin/realms/{realm}/client-scopes/{clientScopeUuid}/protocol-mappers/models/{protocolMapperUuid}", UriKind.Relative);

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