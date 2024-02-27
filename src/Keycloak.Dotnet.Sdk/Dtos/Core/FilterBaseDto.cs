namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Core;

public abstract record FilterBaseDto
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    [JsonPropertyName("search")]
    public string? Search { get; set; }

    [JsonPropertyName("first")]
    public int? PageNumber { get; set; }

    [JsonPropertyName("max")]
    public int? PageSize { get; set; }

    [JsonPropertyName("q")]
    public Dictionary<string, string>? Attributes { get; set; }
}
