namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Roles;

public record RoleFilterDto : FilterBaseDto
{
    [JsonPropertyName("briefRepresentation")]
    public string? BriefRepresentation { get; set; }
}
