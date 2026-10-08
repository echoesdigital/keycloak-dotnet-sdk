using System.Text.Json;
using Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.Realms;
using Echoes.Digital.Keycloak.Dotnet.Sdk.Extensions;
using FluentAssertions;
using Xunit;

namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Tests.Realms;

public class RealmDtoSerializationTests
{
    private static readonly string[] SessionAndBruteForceKeys =
    {
        "ssoSessionIdleTimeout", "ssoSessionMaxLifespan", "offlineSessionIdleTimeout", "offlineSessionMaxLifespanEnabled",
        "offlineSessionMaxLifespan", "bruteForceProtected", "permanentLockout", "maxFailureWaitSeconds",
        "minimumQuickLoginWaitSeconds", "waitIncrementSeconds", "quickLoginCheckMilliSeconds", "maxDeltaTimeSeconds",
        "failureFactor"
    };

    private static string Serialize<T>(T value) => JsonSerializer.Serialize(value, SerializerExtensions.SerializeOptions);

    [Fact]
    public void Session_and_brute_force_fields_are_omitted_when_null()
    {
        // A PUT of a realm with only BrowserFlow must not send the new fields, so Keycloak keeps the current values.
        var json = Serialize(new RealmDto { Realm = "acme", BrowserFlow = "browser-acme" });

        using var doc = JsonDocument.Parse(json);
        doc.RootElement.EnumerateObject().Select(p => p.Name).Should().BeEquivalentTo("realm", "browserFlow");
    }

    [Fact]
    public void Session_and_brute_force_fields_use_keycloak_names()
    {
        var realm = new RealmDto
        {
            SsoSessionIdleTimeout = 1800,
            SsoSessionMaxLifespan = 36000,
            OfflineSessionIdleTimeout = 2592000,
            OfflineSessionMaxLifespanEnabled = true,
            OfflineSessionMaxLifespan = 5184000,
            BruteForceProtected = true,
            PermanentLockout = false,
            MaxFailureWaitSeconds = 900,
            MinimumQuickLoginWaitSeconds = 60,
            WaitIncrementSeconds = 60,
            QuickLoginCheckMilliSeconds = 1000,
            MaxDeltaTimeSeconds = 43200,
            FailureFactor = 30
        };

        using var doc = JsonDocument.Parse(Serialize(realm));
        doc.RootElement.EnumerateObject().Select(p => p.Name).Should().BeEquivalentTo(SessionAndBruteForceKeys);
        doc.RootElement.GetProperty("offlineSessionMaxLifespan").GetInt32().Should().Be(5184000);
        doc.RootElement.GetProperty("offlineSessionMaxLifespanEnabled").GetBoolean().Should().BeTrue();
        doc.RootElement.GetProperty("quickLoginCheckMilliSeconds").GetInt64().Should().Be(1000);
    }

    [Fact]
    public void Realm_representation_from_keycloak_is_read()
    {
        const string json = @"{""realm"":""acme"",""ssoSessionIdleTimeout"":1800,""offlineSessionMaxLifespanEnabled"":false,
            ""bruteForceProtected"":true,""failureFactor"":30,""quickLoginCheckMilliSeconds"":1000}";

        var realm = JsonSerializer.Deserialize<RealmDto>(json);

        realm!.SsoSessionIdleTimeout.Should().Be(1800);
        realm.OfflineSessionMaxLifespanEnabled.Should().BeFalse();
        realm.BruteForceProtected.Should().BeTrue();
        realm.FailureFactor.Should().Be(30);
        realm.QuickLoginCheckMilliSeconds.Should().Be(1000);
        realm.OfflineSessionMaxLifespan.Should().BeNull();
    }
}
