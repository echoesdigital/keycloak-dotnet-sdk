namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.RoleMappers;

public abstract record RoleMappingsDto
{
    [JsonPropertyName("realmMappings")]
    public List<RoleDto> RealmMappings { get; set; }
}
