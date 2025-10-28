namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.AuthenticationFlows;

public record AuthenticationExecutionInfoRepresentationDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("requirement")]
    public string? Requirement { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("alias")]
    public string? Alias { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("requirementChoices")]
    public List<string>? RequirementChoices { get; set; }

    [JsonPropertyName("configurable")]
    public bool? Configurable { get; set; }

    [JsonPropertyName("authenticationFlow")]
    public bool? AuthenticationFlow { get; set; }

    [JsonPropertyName("providerId")]
    public string? ProviderId { get; set; }

    [JsonPropertyName("authenticationConfig")]
    public string? AuthenticationConfig { get; set; }

    [JsonPropertyName("flowId")]
    public string? FlowId { get; set; }

    [JsonPropertyName("level")]
    public int? Level { get; set; }

    [JsonPropertyName("index")]
    public int? Index { get; set; }

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }
}
