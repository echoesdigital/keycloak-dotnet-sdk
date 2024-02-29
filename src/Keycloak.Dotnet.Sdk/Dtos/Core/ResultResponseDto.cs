namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Core;

public record ResultResponseDto
{
    public ResultComposition Result { get; set; } = new();

    public string? Identifier { get; set; }

    public record ResultComposition
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}

public record ResultResponseDto<TData> : ResultResponseDto
{
    public TData? Data { get; set; }
}
