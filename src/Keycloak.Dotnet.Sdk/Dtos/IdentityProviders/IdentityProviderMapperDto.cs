using Echoes.Digital.Keycloak.Dotnet.Sdk.Converters;

namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.IdentityProviders;

public record IdentityProviderMapperDto
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("identityProviderAlias")]
    public string? IdentityProviderAlias { get; init; }

    [JsonPropertyName("identityProviderMapper")]
    public string? IdentityProviderMapper { get; init; }

    [JsonPropertyName("config")]
    public ConfigComposition? Config { get; init; }

    /// <summary>
    /// Mapper configuration. In Keycloak it is a free <c>Map&lt;String,String&gt;</c>.
    /// The typed properties cover the keys used up to 1.0.0; any other key (for example <c>template</c>,
    /// <c>target</c>, <c>attribute.name</c>) goes in <see cref="AdditionalConfig"/>.
    /// On the wire the object is flat: typed keys and additional keys side by side.
    /// </summary>
    [JsonConverter(typeof(IdentityProviderMapperConfigConverter))]
    public record ConfigComposition
    {
        [JsonPropertyName("syncMode")]
        public string? SyncMode { get; init; }

        [JsonPropertyName("claim")]
        public string? Claim { get; init; }

        [JsonPropertyName("are.claim.values.regex")]
        public string? AreClaimValuesRegex { get; init; }

        [JsonPropertyName("role")]
        public string? Role { get; init; }

        [JsonPropertyName("user.attribute")]
        public string? UserAttribute { get; init; }

        [JsonPropertyName("attribute.value")]
        public string? AttributeValue { get; init; }

        [JsonPropertyName("attribute")]
        public string? Attribute { get; init; }

        [JsonPropertyName("claims")]
        public string? Claims { get; init; }

        /// <summary>
        /// Config keys without a typed property. When a key here is also a typed key, the typed property wins
        /// if it is not null.
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, string>? AdditionalConfig { get; init; }

        /// <summary>
        /// Builds the configuration from a free dictionary. Known keys fill the typed properties; the others go to
        /// <see cref="AdditionalConfig"/>.
        /// </summary>
        public static ConfigComposition FromDictionary(IReadOnlyDictionary<string, string?> config)
        {
            return IdentityProviderMapperConfigConverter.Build(config);
        }

        /// <summary>
        /// Returns the whole configuration as a dictionary, typed keys included, exactly as sent to Keycloak.
        /// </summary>
        public Dictionary<string, string> ToDictionary()
        {
            return IdentityProviderMapperConfigConverter.Flatten(this);
        }
    }
}
