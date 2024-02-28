namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Resources;

public class IdentityProvidersResource(KeycloakInstance instance)
{
    public async Task<ResultResponseDto> Create(string realm, IdentityProviderDto identityProvider)
    {
        var uri = new Uri($"admin/realms/{realm}/identity-provider/instances", UriKind.Relative);

        using var response = await instance.HttpClient.PostJsonAsync(uri, identityProvider).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync().ConfigureAwait(false)
            }
        };
    }

    public async Task<ResultResponseDto<IdentityProviderDto>> Get(string realm, string identityProviderUuid)
    {
        var uri = new Uri($"admin/realms/{realm}/identity-provider/instances/{identityProviderUuid}", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<IdentityProviderDto>
        {
            Data = await response.Content.ReadAsJsonAsync<IdentityProviderDto>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<List<IdentityProviderDto>>> List(string realm)
    {
        var uri = new Uri($"admin/realms/{realm}/identity-provider/instances", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<List<IdentityProviderDto>>
        {
            Data = await response.Content.ReadAsJsonAsync<List<IdentityProviderDto>>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Update(string realm, IdentityProviderDto identityProvider)
    {
        var uri = new Uri($"admin/realms/{realm}/identity-provider/instances/{identityProvider.Alias}", UriKind.Relative);

        using var response = await instance.HttpClient.PutJsonAsync(uri, identityProvider).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync().ConfigureAwait(false)
            }
        };
    }

    public async Task<ResultResponseDto> Delete(string realm, string identityProviderAlias)
    {
        var uri = new Uri($"admin/realms/{realm}/identity-provider/instances/{identityProviderAlias}", UriKind.Relative);

        using var response = await instance.HttpClient.DeleteAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync().ConfigureAwait(false)
            }
        };
    }

    public async Task<ResultResponseDto> CreateMapper(string realm, string identityProviderAlias, IdentityProviderMapperDto mapper)
    {
        var uri = new Uri($"admin/realms/{realm}/identity-provider/instances/{identityProviderAlias}/mappers", UriKind.Relative);

        using var response = await instance.HttpClient.PostJsonAsync(uri, mapper).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync().ConfigureAwait(false)
            }
        };
    }

    public async Task<ResultResponseDto<IdentityProviderMapperDto>> GetMapper(string realm, string identityProviderAlias, string mapperUuid)
    {
        var uri = new Uri($"admin/realms/{realm}/identity-provider/instances/{identityProviderAlias}/mappers/{mapperUuid}", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<IdentityProviderMapperDto>
        {
            Data = await response.Content.ReadAsJsonAsync<IdentityProviderMapperDto>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<List<IdentityProviderMapperDto>>> ListMappers(string realm, string identityProviderAlias)
    {
        var uri = new Uri($"admin/realms/{realm}/identity-provider/instances/{identityProviderAlias}/mappers", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<List<IdentityProviderMapperDto>>
        {
            Data = await response.Content.ReadAsJsonAsync<List<IdentityProviderMapperDto>>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> UpdateMapper(string realm, string identityProviderAlias, IdentityProviderMapperDto mapper)
    {
        var uri = new Uri($"admin/realms/{realm}/identity-provider/instances/{identityProviderAlias}/mappers/{mapper.Id}", UriKind.Relative);

        using var response = await instance.HttpClient.PutJsonAsync(uri, mapper).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync().ConfigureAwait(false)
            }
        };
    }

    public async Task<ResultResponseDto> DeleteMapper(string realm, string identityProviderAlias, string mapperUuid)
    {
        var uri = new Uri($"admin/realms/{realm}/identity-provider/instances/{identityProviderAlias}/mappers/{mapperUuid}", UriKind.Relative);

        using var response = await instance.HttpClient.DeleteAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync().ConfigureAwait(false)
            }
        };
    }
}