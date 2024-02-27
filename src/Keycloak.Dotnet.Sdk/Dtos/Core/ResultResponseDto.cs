namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Core;

public record ResultResponseDto
{
    public ResultAggregation Result { get; set; } = new();

    public record ResultAggregation
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}

public record ResultResponseDto<TData> : ResultResponseDto
{
    public TData? Data { get; set; }
}
