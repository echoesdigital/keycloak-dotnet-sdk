namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.RoleMappers;

public record RoleDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("composite")]
    public bool? Composite { get; set; }

    [JsonPropertyName("clientRole")]
    public bool? ClientRole { get; set; }

    [JsonPropertyName("containerId")]
    public string? ContainerId { get; set; }
}
