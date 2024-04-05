namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.UsersProfiles;

public record UPConfigDto
{
    [JsonPropertyName("attributes")]
    public List<UPAttribute>? Attributes { get; init; }

    [JsonPropertyName("groups")]
    public List<UPGroup>? Groups { get; init; }

    public record UPAttribute
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; init; }

        [JsonPropertyName("validations")]
        public Dictionary<string, object>? Validations { get; init; }

        [JsonPropertyName("annotations")]
        public Dictionary<string, object>? Annotations { get; init; }

        [JsonPropertyName("required")]
        public UPAttributeRequired? Required { get; init; }

        [JsonPropertyName("permissions")]
        public UPAttributePermissions? Permissions { get; init; }

        [JsonPropertyName("selector")]
        public UPAttributeSelector? Selector { get; init; }

        [JsonPropertyName("group")]
        public string? Group { get; init; }

        [JsonPropertyName("multivalued")]
        public bool? Multivalued { get; init; }
    }

    public record UPAttributePermissions
    {
        [JsonPropertyName("view")]
        public HashSet<string>? View { get; init; }

        [JsonPropertyName("edit")]
        public HashSet<string>? Edit { get; init; }
    }

    public record UPAttributeRequired
    {
        [JsonPropertyName("roles")]
        public HashSet<string>? Roles { get; init; }

        [JsonPropertyName("scopes")]
        public HashSet<string>? Scopes { get; init; }
    }

    public record UPAttributeSelector
    {
        [JsonPropertyName("scopes")]
        public HashSet<string>? Scopes { get; init; }
    }

    public record UPGroup
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("displayHeader")]
        public string? DisplayHeader { get; init; }

        [JsonPropertyName("displayDescription")]
        public string? DisplayDescription { get; init; }

        [JsonPropertyName("annotations")]
        public Dictionary<string, object>? Annotations { get; init; }
    }
}
