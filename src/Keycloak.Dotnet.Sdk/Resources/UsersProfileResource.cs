using Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.UsersProfiles;

namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Resources;

public class UsersProfileResource
{
    private readonly KeycloakInstance _instance;

    public UsersProfileResource(KeycloakInstance instance)
    {
        _instance = instance;
    }

    public async Task<ResultResponseDto> Get(string realm)
    {
        var uri = new Uri($"admin/realms/{realm}/users/profile", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<UPConfigDto>
        {
            Data = await response.Content.ReadAsJsonAsync<UPConfigDto>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    public async Task<ResultResponseDto> Update(string realm, UPConfigDto upConfigDto)
    {
        var uri = new Uri($"admin/realms/{realm}/users/profile", UriKind.Relative);

        using var response = await _instance.HttpClient.PutJsonAsync(uri, upConfigDto).ConfigureAwait(false);

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