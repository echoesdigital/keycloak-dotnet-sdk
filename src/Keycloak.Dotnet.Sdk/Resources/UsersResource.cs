namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Resources;

public class UsersResource
{
    private readonly KeycloakInstance _instance;

    public UsersResource(KeycloakInstance instance)
    {
        _instance = instance;
    }

    public async Task<ResultResponseDto> Create(string realm, UserDto user)
    {
        var uri = new Uri($"admin/realms/{realm}/users", UriKind.Relative);

        using var response = await _instance.HttpClient.PostJsonAsync(uri, user).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync().ConfigureAwait(false)
            },
            Identifier = response.Headers.Location?.Segments.LastOrDefault()
        };
    }

    public async Task<ResultResponseDto<UserDto>> Get(string realm, string userId)
    {
        var uri = new Uri($"admin/realms/{realm}/users/{userId}", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<UserDto>
        {
            Data = await response.Content.ReadAsJsonAsync<UserDto>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<List<UserDto>>> List(string realm, UserFilterDto filter)
    {
        var uri = new Uri($"admin/realms/{realm}/users{filter.ToQueryString()}", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<List<UserDto>>
        {
            Data = await response.Content.ReadAsJsonAsync<List<UserDto>>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<int>> Count(string realm, UserFilterDto filter)
    {
        var uri = new Uri($"admin/realms/{realm}/users/count{filter.ToQueryString()}", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<int>
        {
            Data = await response.Content.ReadAsJsonAsync<int>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Update(string realm, UserDto user)
    {
        var uri = new Uri($"admin/realms/{realm}/users/{user.Id}", UriKind.Relative);

        using var response = await _instance.HttpClient.PutJsonAsync(uri, user).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync().ConfigureAwait(false)
            }
        };
    }

    public async Task<ResultResponseDto> Delete(string realm, string id)
    {
        var uri = new Uri($"admin/realms/{realm}/users/{id}", UriKind.Relative);

        using var response = await _instance.HttpClient.DeleteAsync(uri).ConfigureAwait(false);

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
