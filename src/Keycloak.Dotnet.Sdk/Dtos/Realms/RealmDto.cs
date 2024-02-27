namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Realms;

public record RealmDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("realm")]
    public string? Realm { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("displayNameHtml")]
    public string? DisplayNameHtml { get; set; }

    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    [JsonPropertyName("defaultRoles")]
    public string[]? DefaultRoles { get; set; }

    [JsonPropertyName("requiredCredentials")]
    public string[]? RequiredCredentials { get; set; }

    [JsonPropertyName("rememberMe")]
    public bool? RememberMe { get; set; }

    [JsonPropertyName("loginWithEmailAllowed")]
    public bool? LoginWithEmailAllowed { get; set; }

    [JsonPropertyName("internationalizationEnabled")]
    public bool? InternationalizationEnabled { get; set; }

    [JsonPropertyName("supportedLocales")]
    public string[]? SupportedLocales { get; set; }

    [JsonPropertyName("defaultLocale")]
    public string? DefaultLocale { get; set; }

    [JsonPropertyName("attributes")]
    public Dictionary<string, string>? Attributes { get; set; }
}