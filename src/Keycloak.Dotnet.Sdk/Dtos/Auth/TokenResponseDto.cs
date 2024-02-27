namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Auth;

public record TokenResponseDto
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
}
