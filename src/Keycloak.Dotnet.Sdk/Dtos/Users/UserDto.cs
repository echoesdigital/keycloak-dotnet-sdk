namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Users;

public record UserDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("emailVerified")]
    public bool? EmailVerified { get; set; }

    [JsonPropertyName("attributes")]
    public Dictionary<string, List<string>>? Attributes { get; set; }

    [JsonPropertyName("credentials")]
    public List<CredentialsDto>? Credentials { get; set; }

    public record CredentialsDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("userLabel")]
        public string? UserLabel { get; init; }

        [JsonPropertyName("createdDate")]
        public long? CreatedDate { get; init; }

        [JsonPropertyName("secretData")]
        public string? SecretData { get; init; }

        [JsonPropertyName("credentialData")]
        public string? CredentialData { get; init; }

        [JsonPropertyName("priority")]
        public int? Priority { get; init; }

        [JsonPropertyName("value")]
        public string? Value { get; init; }

        [JsonPropertyName("temporary")]
        public bool? Temporary { get; init; }

        [JsonPropertyName("device")]
        public string? Device { get; init; }

        [JsonPropertyName("hashedSaltedValue")]
        public string? HashedSaltedValue { get; init; }

        [JsonPropertyName("salt")]
        public string? Salt { get; init; }

        [JsonPropertyName("hashIterations")]
        public int? HashIterations { get; init; }

        [JsonPropertyName("counter")]
        public int? Counter { get; init; }

        [JsonPropertyName("algorithm")]
        public string? Algorithm { get; init; }

        [JsonPropertyName("digits")]
        public int? Digits { get; init; }

        [JsonPropertyName("period")]
        public int? Period { get; init; }

        [JsonPropertyName("config")]
        public Dictionary<string, object>? Config { get; init; }
    }
}
