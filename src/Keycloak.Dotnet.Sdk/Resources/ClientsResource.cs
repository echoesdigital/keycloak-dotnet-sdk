namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Resources;

public class ClientsResource(KeycloakInstance instance)
{
    public async Task<ResultResponseDto> Create(string realm, ClientDto client)
    {
        var uri = new Uri($"admin/realms/{realm}/clients", UriKind.Relative);

        using var response = await instance.HttpClient.PostJsonAsync(uri, client).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<ClientDto>> Get(string realm, string id)
    {
        var uri = new Uri($"admin/realms/{realm}/clients/{id}", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<ClientDto>
        {
            Data = await response.Content.ReadAsJsonAsync<ClientDto>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<List<ClientDto>>> List(string realm, ClientFilterDto filter)
    {
        var uri = new Uri($"admin/realms/{realm}/clients{filter.ToQueryString()}", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<List<ClientDto>>
        {
            Data = await response.Content.ReadAsJsonAsync<List<ClientDto>>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Update(string realm, ClientDto client)
    {
        var uri = new Uri($"admin/realms/{realm}/clients/{client.Id}", UriKind.Relative);

        using var response = await instance.HttpClient.PutJsonAsync(uri, client).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Delete(string realm, string id)
    {
        var uri = new Uri($"admin/realms/{realm}/clients/{id}", UriKind.Relative);

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
