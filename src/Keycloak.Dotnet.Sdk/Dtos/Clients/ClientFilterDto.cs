namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Clients;

public record ClientFilterDto : FilterBaseDto
{
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }
}
