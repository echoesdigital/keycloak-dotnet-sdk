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

    [JsonPropertyName("browserSecurityHeaders")]
    public BrowserSecurityHeaders? BrowserSecurityHeaders { get; set; }

    [JsonPropertyName("loginTheme")]
    public string? LoginTheme { get; set; }

    [JsonPropertyName("accountTheme")]
    public string? AccountTheme { get; set; }

    [JsonPropertyName("adminTheme")]
    public string? AdminTheme { get; set; }

    [JsonPropertyName("emailTheme")]
    public string? EmailTheme { get; set; }

    [JsonPropertyName("browserFlow")]
    public string? BrowserFlow { get; set; }

    // Session. Values in seconds. Null is not serialized, so a PUT with a partial DTO keeps the current value.

    [JsonPropertyName("ssoSessionIdleTimeout")]
    public int? SsoSessionIdleTimeout { get; set; }

    [JsonPropertyName("ssoSessionMaxLifespan")]
    public int? SsoSessionMaxLifespan { get; set; }

    [JsonPropertyName("offlineSessionIdleTimeout")]
    public int? OfflineSessionIdleTimeout { get; set; }

    [JsonPropertyName("offlineSessionMaxLifespanEnabled")]
    public bool? OfflineSessionMaxLifespanEnabled { get; set; }

    [JsonPropertyName("offlineSessionMaxLifespan")]
    public int? OfflineSessionMaxLifespan { get; set; }

    // Brute force detection.

    [JsonPropertyName("bruteForceProtected")]
    public bool? BruteForceProtected { get; set; }

    [JsonPropertyName("permanentLockout")]
    public bool? PermanentLockout { get; set; }

    [JsonPropertyName("maxFailureWaitSeconds")]
    public int? MaxFailureWaitSeconds { get; set; }

    [JsonPropertyName("minimumQuickLoginWaitSeconds")]
    public int? MinimumQuickLoginWaitSeconds { get; set; }

    [JsonPropertyName("waitIncrementSeconds")]
    public int? WaitIncrementSeconds { get; set; }

    [JsonPropertyName("quickLoginCheckMilliSeconds")]
    public long? QuickLoginCheckMilliSeconds { get; set; }

    [JsonPropertyName("maxDeltaTimeSeconds")]
    public int? MaxDeltaTimeSeconds { get; set; }

    [JsonPropertyName("failureFactor")]
    public int? FailureFactor { get; set; }
}