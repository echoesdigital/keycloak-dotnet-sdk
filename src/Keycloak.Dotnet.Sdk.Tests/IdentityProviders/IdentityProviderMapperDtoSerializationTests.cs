using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.IdentityProviders;
using Echoes.Digital.Keycloak.Dotnet.Sdk.Extensions;
using FluentAssertions;
using Xunit;

namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Tests.IdentityProviders;

public class IdentityProviderMapperDtoSerializationTests
{
    // Same options PostJsonAsync / PutJsonAsync use to send the body to Keycloak.
    private static string Serialize<T>(T value) => JsonSerializer.Serialize(value, SerializerExtensions.SerializeOptions);

    private static async Task<T?> ReadLikeResource<T>(string json)
    {
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        return await content.ReadAsJsonAsync<T>();
    }

    public static IEnumerable<object[]> MappersInUseByTenants()
    {
        // The four mappers trisky-desktop-tenants creates today (TenantsService.Keycloak.cs, PersistIdpMappersAsync).
        var claims = JsonSerializer.Serialize(new List<KeyValuePair<string, string>> { new("key", "groups"), new("value", "placeholder") });

        yield return new object[]
        {
            "tenant", "hardcoded-attribute-idp-mapper",
            new LegacyConfig { SyncMode = "INHERIT", Attribute = "tenant", AttributeValue = "acme" },
            new IdentityProviderMapperDto.ConfigComposition { SyncMode = "INHERIT", Attribute = "tenant", AttributeValue = "acme" }
        };
        yield return new object[]
        {
            "Username", "oidc-user-attribute-idp-mapper",
            new LegacyConfig { SyncMode = "INHERIT", Claim = "preferred_username", UserAttribute = "username" },
            new IdentityProviderMapperDto.ConfigComposition { SyncMode = "INHERIT", Claim = "preferred_username", UserAttribute = "username" }
        };
        yield return new object[]
        {
            "Email", "oidc-user-attribute-idp-mapper",
            new LegacyConfig { SyncMode = "INHERIT", Claim = "preferred_username", UserAttribute = "email" },
            new IdentityProviderMapperDto.ConfigComposition { SyncMode = "INHERIT", Claim = "preferred_username", UserAttribute = "email" }
        };
        yield return new object[]
        {
            "Administrators Group", "oidc-advanced-role-idp-mapper",
            new LegacyConfig { Claims = claims, SyncMode = "INHERIT", AreClaimValuesRegex = "true", Role = "administrators" },
            new IdentityProviderMapperDto.ConfigComposition { Claims = claims, SyncMode = "INHERIT", AreClaimValuesRegex = "true", Role = "administrators" }
        };
    }

    [Theory]
    [MemberData(nameof(MappersInUseByTenants))]
    public void Typed_properties_produce_the_same_json_as_version_1_0_0(string name, string mapperType, LegacyConfig legacy, IdentityProviderMapperDto.ConfigComposition current)
    {
        var legacyJson = Serialize(new LegacyMapper { Name = name, IdentityProviderAlias = "acme", IdentityProviderMapper = mapperType, Config = legacy });
        var currentJson = Serialize(new IdentityProviderMapperDto { Name = name, IdentityProviderAlias = "acme", IdentityProviderMapper = mapperType, Config = current });

        currentJson.Should().Be(legacyJson);
    }

    [Fact]
    public void Free_keys_are_written_flat_inside_config()
    {
        var mapper = new IdentityProviderMapperDto
        {
            Name = "Username",
            IdentityProviderMapper = "oidc-username-idp-mapper",
            Config = new IdentityProviderMapperDto.ConfigComposition
            {
                SyncMode = "IMPORT",
                AdditionalConfig = new Dictionary<string, string>
                {
                    ["template"] = "${CLAIM.preferred_username}@${CLAIM.domain}",
                    ["target"] = "LOCAL"
                }
            }
        };

        using var doc = JsonDocument.Parse(Serialize(mapper));
        var config = doc.RootElement.GetProperty("config");

        config.EnumerateObject().Select(p => p.Name).Should().Equal("syncMode", "template", "target");
        config.GetProperty("template").GetString().Should().Be("${CLAIM.preferred_username}@${CLAIM.domain}");
        config.GetProperty("target").GetString().Should().Be("LOCAL");
        config.TryGetProperty("AdditionalConfig", out _).Should().BeFalse();
        config.TryGetProperty("additionalConfig", out _).Should().BeFalse();
    }

    [Fact]
    public async Task Free_keys_are_read_into_AdditionalConfig_and_known_keys_into_typed_properties()
    {
        const string json = @"{
              ""id"": ""m1"",
              ""name"": ""Group"",
              ""identityProviderAlias"": ""acme"",
              ""identityProviderMapper"": ""saml-advanced-role-idp-mapper"",
              ""config"": {
                ""syncMode"": ""FORCE"",
                ""role"": ""operators"",
                ""attribute.name"": ""memberOf"",
                ""attribute.friendly.name"": ""groups""
              }
            }";

        var mapper = await ReadLikeResource<IdentityProviderMapperDto>(json);

        mapper!.Config!.SyncMode.Should().Be("FORCE");
        mapper.Config.Role.Should().Be("operators");
        mapper.Config.AdditionalConfig.Should().BeEquivalentTo(new Dictionary<string, string>
        {
            ["attribute.name"] = "memberOf",
            ["attribute.friendly.name"] = "groups"
        });
    }

    [Fact]
    public async Task Read_then_write_keeps_every_key()
    {
        const string config = @"{""syncMode"":""FORCE"",""claim"":""email"",""user.attribute"":""email"",""template"":""x"",""target"":""LOCAL""}";

        var read = await ReadLikeResource<IdentityProviderMapperDto.ConfigComposition>(config);
        var written = Serialize(read);

        JsonSerializer.Deserialize<Dictionary<string, string>>(written).Should().BeEquivalentTo(
            JsonSerializer.Deserialize<Dictionary<string, string>>(config));
    }

    [Fact]
    public void Typed_property_wins_over_the_same_key_in_AdditionalConfig()
    {
        var config = new IdentityProviderMapperDto.ConfigComposition
        {
            SyncMode = "FORCE",
            AdditionalConfig = new Dictionary<string, string> { ["syncMode"] = "LEGACY", ["claim"] = "upn" }
        };

        config.ToDictionary().Should().BeEquivalentTo(new Dictionary<string, string> { ["syncMode"] = "FORCE", ["claim"] = "upn" });

        using var doc = JsonDocument.Parse(Serialize(config));
        doc.RootElement.EnumerateObject().Count(p => p.Name == "syncMode").Should().Be(1);
    }

    [Fact]
    public void FromDictionary_splits_typed_and_free_keys()
    {
        var config = IdentityProviderMapperDto.ConfigComposition.FromDictionary(new Dictionary<string, string?>
        {
            ["syncMode"] = "IMPORT",
            ["user.attribute"] = "username",
            ["template"] = "${CLAIM.sub}"
        });

        config.SyncMode.Should().Be("IMPORT");
        config.UserAttribute.Should().Be("username");
        config.AdditionalConfig.Should().BeEquivalentTo(new Dictionary<string, string> { ["template"] = "${CLAIM.sub}" });
    }

    [Fact]
    public void Null_values_are_not_written()
    {
        var json = Serialize(new IdentityProviderMapperDto { Name = "n", Config = new IdentityProviderMapperDto.ConfigComposition { SyncMode = "FORCE" } });

        using var doc = JsonDocument.Parse(json);
        doc.RootElement.GetProperty("config").EnumerateObject().Select(p => p.Name).Should().Equal("syncMode");
        doc.RootElement.TryGetProperty("id", out _).Should().BeFalse();

        Serialize(new IdentityProviderMapperDto { Name = "n" }).Should().NotContain("config");
    }

    // Copy of the 1.0.0 shape, used as the compatibility reference.
    public record LegacyMapper
    {
        [JsonPropertyName("id")] public string? Id { get; init; }
        [JsonPropertyName("name")] public string? Name { get; init; }
        [JsonPropertyName("identityProviderAlias")] public string? IdentityProviderAlias { get; init; }
        [JsonPropertyName("identityProviderMapper")] public string? IdentityProviderMapper { get; init; }
        [JsonPropertyName("config")] public LegacyConfig? Config { get; init; }
    }

    public record LegacyConfig
    {
        [JsonPropertyName("syncMode")] public string? SyncMode { get; init; }
        [JsonPropertyName("claim")] public string? Claim { get; init; }
        [JsonPropertyName("are.claim.values.regex")] public string? AreClaimValuesRegex { get; init; }
        [JsonPropertyName("role")] public string? Role { get; init; }
        [JsonPropertyName("user.attribute")] public string? UserAttribute { get; init; }
        [JsonPropertyName("attribute.value")] public string? AttributeValue { get; init; }
        [JsonPropertyName("attribute")] public string? Attribute { get; init; }
        [JsonPropertyName("claims")] public string? Claims { get; init; }
    }
}
