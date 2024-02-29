namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Resources;

public class UserRoleMappingsResource
{
    private readonly KeycloakInstance _instance;

    public UserRoleMappingsResource(KeycloakInstance instance)
    {
        _instance = instance;
    }

    public async Task<ResultResponseDto> Create(string realm, string userId, List<RoleDto> roles)
    {
        var uri = new Uri($"admin/realms/{realm}/users/{userId}/role-mappings/realm", UriKind.Relative);

        using var response = await _instance.HttpClient.PostJsonAsync(uri, roles).ConfigureAwait(false);

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

    public async Task<ResultResponseDto<RoleMappingsDto>> Get(string realm, string userId)
    {
        var uri = new Uri($"admin/realms/{realm}/users/{userId}/role-mappings", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<RoleMappingsDto>
        {
            Data = await response.Content.ReadAsJsonAsync<RoleMappingsDto>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Delete(string realm, string userId, List<RoleDto> roles)
    {
        var uri = new Uri($"admin/realms/{realm}/users/{userId}/role-mappings/realm", UriKind.Relative);

        using var response = await _instance.HttpClient.DeleteJsonAsync(uri, roles).ConfigureAwait(false);

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
