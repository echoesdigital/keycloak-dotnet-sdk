namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Users;

public record UserFilterDto : FilterBaseDto
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("exact")]
    public bool? Exact { get; set; }
}
