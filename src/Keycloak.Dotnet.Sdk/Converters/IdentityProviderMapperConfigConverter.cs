using ConfigComposition = Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.IdentityProviders.IdentityProviderMapperDto.ConfigComposition;

namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Converters;

/// <summary>
/// Reads and writes the identity provider mapper config as the flat string map Keycloak uses, keeping the typed
/// properties of <see cref="ConfigComposition"/> and carrying any other key in
/// <see cref="ConfigComposition.AdditionalConfig"/>. Typed keys are written first, in the same order as in 1.0.0;
/// null values are never written, so a PUT never sends <c>"key": null</c>.
/// </summary>
internal sealed class IdentityProviderMapperConfigConverter : JsonConverter<ConfigComposition>
{
    private const string SyncModeKey = "syncMode";
    private const string ClaimKey = "claim";
    private const string AreClaimValuesRegexKey = "are.claim.values.regex";
    private const string RoleKey = "role";
    private const string UserAttributeKey = "user.attribute";
    private const string AttributeValueKey = "attribute.value";
    private const string AttributeKey = "attribute";
    private const string ClaimsKey = "claims";

    private static readonly HashSet<string> TypedKeys = new(StringComparer.Ordinal)
    {
        SyncModeKey, ClaimKey, AreClaimValuesRegexKey, RoleKey, UserAttributeKey, AttributeValueKey, AttributeKey, ClaimsKey
    };

    public override ConfigComposition? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) return null;
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException("Mapper config must be a JSON object.");

        var values = new Dictionary<string, string?>(StringComparer.Ordinal);

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject) return Build(values);
            if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException("Invalid mapper config.");

            var key = reader.GetString()!;
            reader.Read();

            values[key] = reader.TokenType switch
            {
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.Null => null,
                _ => JsonElement.ParseValue(ref reader).GetRawText()
            };
        }

        throw new JsonException("Unexpected end of mapper config.");
    }

    public override void Write(Utf8JsonWriter writer, ConfigComposition value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var (key, configValue) in Flatten(value))
        {
            writer.WriteString(key, configValue);
        }

        writer.WriteEndObject();
    }

    internal static ConfigComposition Build(IReadOnlyDictionary<string, string?> values)
    {
        string? Take(string key) => values.TryGetValue(key, out var v) ? v : null;

        var additional = values
            .Where(w => !TypedKeys.Contains(w.Key) && w.Value != null)
            .ToDictionary(k => k.Key, v => v.Value!, StringComparer.Ordinal);

        return new ConfigComposition
        {
            SyncMode = Take(SyncModeKey),
            Claim = Take(ClaimKey),
            AreClaimValuesRegex = Take(AreClaimValuesRegexKey),
            Role = Take(RoleKey),
            UserAttribute = Take(UserAttributeKey),
            AttributeValue = Take(AttributeValueKey),
            Attribute = Take(AttributeKey),
            Claims = Take(ClaimsKey),
            AdditionalConfig = additional.Count > 0 ? additional : null
        };
    }

    internal static Dictionary<string, string> Flatten(ConfigComposition value)
    {
        // Insertion order is kept by Dictionary while nothing is removed, so the JSON keeps the 1.0.0 key order.
        var result = new Dictionary<string, string>(StringComparer.Ordinal);

        void Put(string key, string? v)
        {
            if (v != null) result[key] = v;
        }

        Put(SyncModeKey, value.SyncMode);
        Put(ClaimKey, value.Claim);
        Put(AreClaimValuesRegexKey, value.AreClaimValuesRegex);
        Put(RoleKey, value.Role);
        Put(UserAttributeKey, value.UserAttribute);
        Put(AttributeValueKey, value.AttributeValue);
        Put(AttributeKey, value.Attribute);
        Put(ClaimsKey, value.Claims);

        if (value.AdditionalConfig == null) return result;

        foreach (var (key, v) in value.AdditionalConfig)
        {
            if (v != null && !result.ContainsKey(key)) result[key] = v;
        }

        return result;
    }
}
