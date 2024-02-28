namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.IdentityProviders;

public record IdentityProviderMapperDto
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("category")]
    public string? Category { get; init; }

    [JsonPropertyName("helpText")]
    public string? HelpText { get; init; }

    [JsonPropertyName("properties")]
    public List<PropertyComposition>? Properties { get; init; }

    public record PropertyComposition
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("label")]
        public string? Label { get; init; }

        [JsonPropertyName("helpText")]
        public string? HelpText { get; init; }

        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("defaultValue")]
        public object? DefaultValue { get; init; }

        [JsonPropertyName("options")]
        public List<string>? Options { get; init; }

        [JsonPropertyName("secret")]
        public bool? Secret { get; init; }

        [JsonPropertyName("readOnly")]
        public bool? ReadOnly { get; init; }
    }
}