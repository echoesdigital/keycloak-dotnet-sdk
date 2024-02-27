namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.RoleMappers;

public record RoleFilterDto : FilterBaseDto
{
    [JsonPropertyName("briefRepresentation")]
    public string? BriefRepresentation { get; set; }
}
