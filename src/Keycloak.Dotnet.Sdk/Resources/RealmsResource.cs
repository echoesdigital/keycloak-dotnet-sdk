namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Resources;

public class RealmsResource(KeycloakInstance instance)
{
    public async Task<ResultResponseDto> Create(RealmDto realm)
    {
        ArgumentNullException.ThrowIfNull(realm.Realm);
        var uri = new Uri($"admin/realms", UriKind.Relative);

        using var response = await instance.HttpClient.PostJsonAsync(uri, realm).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<RealmDto>> Get(string realm)
    {
        var uri = new Uri($"admin/realms/{realm}", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<RealmDto>
        {
            Data = await response.Content.ReadAsJsonAsync<RealmDto>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<List<RealmDto>>> List()
    {
        var uri = new Uri($"admin/realms", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<List<RealmDto>>
        {
            Data = await response.Content.ReadAsJsonAsync<List<RealmDto>>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Update(RealmDto realm)
    {
        var uri = new Uri($"admin/realms/{realm.Realm}", UriKind.Relative);
        using var response = await instance.HttpClient.PutJsonAsync(uri, realm).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Delete(string realm)
    {
        var uri = new Uri($"admin/realms/{realm}", UriKind.Relative);

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