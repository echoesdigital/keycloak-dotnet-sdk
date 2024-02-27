namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Resources;

public class UserRoleMappingsResource(KeycloakInstance instance)
{
    public async Task<ResultResponseDto> Create(string realm, string userId, List<RoleDto> roles)
    {
        var uri = new Uri($"admin/realms/{realm}/users/{userId}/role-mappings/realm", UriKind.Relative);

        using var response = await instance.HttpClient.PostJsonAsync(uri, roles).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync().ConfigureAwait(false)
            }
        };
    }

    public async Task<ResultResponseDto<RoleMappingsDto>> Get(string realm, string userId)
    {
        var uri = new Uri($"admin/realms/{realm}/users/{userId}/role-mappings", UriKind.Relative);

        using var response = await instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

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

        using var response = await instance.HttpClient.DeleteJsonAsync(uri, roles).ConfigureAwait(false);

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
