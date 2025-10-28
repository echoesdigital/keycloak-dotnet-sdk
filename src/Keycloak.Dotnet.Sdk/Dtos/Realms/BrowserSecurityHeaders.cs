namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Realms;

public record BrowserSecurityHeaders
{
    [JsonPropertyName("xFrameOptions")]
    public string? XFrameOptions { get; set; }

    [JsonPropertyName("contentSecurityPolicy")]
    public string? ContentSecurityPolicy { get; set; }

    [JsonPropertyName("contentSecurityPolicyReportOnly")]
    public string? ContentSecurityPolicyReportOnly { get; set; }

    [JsonPropertyName("xContentTypeOptions")]
    public string? XContentTypeOptions { get; set; }

    [JsonPropertyName("xRobotsTag")]
    public string? XRobotsTag { get; set; }

    [JsonPropertyName("xXSSProtection")]
    public string? XXSSProtection { get; set; }

    [JsonPropertyName("strictTransportSecurity")]
    public string? StrictTransportSecurity { get; set; }

    [JsonPropertyName("referrerPolicy")]
    public string? ReferrerPolicy { get; set; }
}
