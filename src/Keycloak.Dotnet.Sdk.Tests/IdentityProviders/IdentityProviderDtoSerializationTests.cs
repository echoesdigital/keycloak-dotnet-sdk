using System.Text;
using System.Text.Json;
using Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.IdentityProviders;
using Echoes.Digital.Keycloak.Dotnet.Sdk.Extensions;
using FluentAssertions;
using Xunit;

namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Tests.IdentityProviders;

public class IdentityProviderDtoSerializationTests
{
    private static string Serialize<T>(T value) => JsonSerializer.Serialize(value, SerializerExtensions.SerializeOptions);

    [Fact]
    public void HideOnLogin_is_omitted_when_null()
    {
        var json = Serialize(new IdentityProviderDto { Alias = "acme", ProviderId = "oidc" });

        using var doc = JsonDocument.Parse(json);
        doc.RootElement.TryGetProperty("hideOnLogin", out _).Should().BeFalse();
    }

    [Fact]
    public void HideOnLogin_is_written_as_top_level_property()
    {
        var json = Serialize(new IdentityProviderDto { Alias = "acme", HideOnLogin = true });

        using var doc = JsonDocument.Parse(json);
        doc.RootElement.GetProperty("hideOnLogin").GetBoolean().Should().BeTrue();
    }

    [Fact]
    public async Task HideOnLogin_and_free_config_are_read_back()
    {
        const string json = @"{""alias"":""acme"",""providerId"":""oidc"",""hideOnLogin"":true,
            ""config"":{""issuer"":""https://idp.example.com"",""pkceEnabled"":""true"",""pkceMethod"":""S256""}}";

        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var idp = await content.ReadAsJsonAsync<IdentityProviderDto>();

        idp!.HideOnLogin.Should().BeTrue();
        idp.Config.Should().Contain("pkceMethod", "S256").And.Contain("issuer", "https://idp.example.com");
    }
}
