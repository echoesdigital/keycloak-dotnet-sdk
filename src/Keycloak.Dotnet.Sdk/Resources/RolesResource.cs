using Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.RoleMappers;

namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Resources;

public class RolesResource
{
    private readonly KeycloakInstance _instance;

    public RolesResource(KeycloakInstance instance)
    {
        _instance = instance;
    }

    public async Task<ResultResponseDto> Create(string realm, string clientUuid, RoleDto role)
    {
        var uri = new Uri($"admin/realms/{realm}/clients/{clientUuid}/roles", UriKind.Relative);

        using var response = await _instance.HttpClient.PostJsonAsync(uri, role).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            },
            Identifier = response.Headers.Location?.Segments.LastOrDefault()
        };
    }

    public async Task<ResultResponseDto<RoleDto>> Get(string realm, string clientUuid, string roleId)
    {
        var uri = new Uri($"admin/realms/{realm}/clients/{clientUuid}/roles/{roleId}", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<RoleDto>
        {
            Data = await response.Content.ReadAsJsonAsync<RoleDto>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto<List<RoleDto>>> List(string realm, string clientId, RoleFilterDto? filter = default)
    {
        var uri = new Uri($"admin/realms/{realm}/clients/{clientId}/roles{filter.ToQueryString()}", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<List<RoleDto>>
        {
            Data = await response.Content.ReadAsJsonAsync<List<RoleDto>>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Update(string realm, string clientUuid, RoleDto role)
    {
        var uri = new Uri($"admin/realms/{realm}/clients/{clientUuid}/roles/{role.Id}", UriKind.Relative);

        using var response = await _instance.HttpClient.PutJsonAsync(uri, role).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Delete(string realm, string clientUuid, string roleId)
    {
        var uri = new Uri($"admin/realms/{realm}/clients/{clientUuid}/roles/{roleId}", UriKind.Relative);

        using var response = await _instance.HttpClient.DeleteAsync(uri).ConfigureAwait(false);

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
