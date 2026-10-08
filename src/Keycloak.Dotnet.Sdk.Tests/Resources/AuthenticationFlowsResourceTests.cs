using System.Net;
using System.Text;
using System.Text.Json;
using Echoes.Digital.Keycloak.Dotnet.Sdk.Dtos.AuthenticationFlows;
using FluentAssertions;
using Xunit;

namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Tests.Resources;

public class AuthenticationFlowsResourceTests
{
    private const string BaseAddress = "https://kc.test/";

    private static (KeycloakInstance Instance, StubHandler Handler) Create(Func<HttpRequestMessage, HttpResponseMessage> respond)
    {
        var handler = new StubHandler(respond);
        return (new KeycloakInstance(new HttpClient(handler) { BaseAddress = new Uri(BaseAddress) }), handler);
    }

    private static HttpResponseMessage Json(string json, HttpStatusCode status = HttpStatusCode.OK) =>
        new(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };

    [Fact]
    public async Task GetByAlias_lists_flows_and_returns_the_one_with_the_alias()
    {
        var (keycloak, handler) = Create(_ => Json(@"[{""id"":""f1"",""alias"":""browser""},{""id"":""f2"",""alias"":""first broker login""}]"));

        var result = await keycloak.AuthenticationFlows.GetByAlias("acme", "first broker login");

        handler.Requests.Should().ContainSingle();
        handler.Requests[0].Method.Should().Be(HttpMethod.Get);
        handler.Requests[0].Uri.Should().Be($"{BaseAddress}admin/realms/acme/authentication/flows");
        result.Result.Success.Should().BeTrue();
        result.Data!.Id.Should().Be("f2");
    }

    [Fact]
    public async Task GetByAlias_returns_success_without_data_when_alias_does_not_exist()
    {
        var (keycloak, _) = Create(_ => Json(@"[{""id"":""f1"",""alias"":""browser""}]"));

        var result = await keycloak.AuthenticationFlows.GetByAlias("acme", "Browser");

        result.Result.Success.Should().BeTrue();
        result.Data.Should().BeNull();
    }

    [Fact]
    public async Task GetByAlias_propagates_list_failure()
    {
        var (keycloak, _) = Create(_ => Json(@"{""error"":""forbidden""}", HttpStatusCode.Forbidden));

        var result = await keycloak.AuthenticationFlows.GetByAlias("acme", "browser");

        result.Result.Success.Should().BeFalse();
        result.Result.Message.Should().Contain("forbidden");
        result.Data.Should().BeNull();
    }

    [Fact]
    public async Task GetAuthenticatorConfig_reads_config_by_id()
    {
        var (keycloak, handler) = Create(_ => Json(@"{""id"":""c1"",""alias"":""redirector"",""config"":{""defaultProvider"":""acme-idp""}}"));

        var result = await keycloak.AuthenticationFlows.GetAuthenticatorConfig("acme", "c1");

        handler.Requests[0].Method.Should().Be(HttpMethod.Get);
        handler.Requests[0].Uri.Should().Be($"{BaseAddress}admin/realms/acme/authentication/config/c1");
        result.Result.Success.Should().BeTrue();
        result.Data!.Config.Should().Contain("defaultProvider", "acme-idp");
    }

    [Fact]
    public async Task GetAuthenticatorConfig_returns_no_data_on_404()
    {
        var (keycloak, _) = Create(_ => Json(@"{""error"":""Could not find authenticator config""}", HttpStatusCode.NotFound));

        var result = await keycloak.AuthenticationFlows.GetAuthenticatorConfig("acme", "missing");

        result.Result.Success.Should().BeFalse();
        result.Data.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAuthenticatorConfig_puts_config_by_id()
    {
        var (keycloak, handler) = Create(_ => new HttpResponseMessage(HttpStatusCode.NoContent));

        var result = await keycloak.AuthenticationFlows.UpdateAuthenticatorConfig("acme", new AuthenticatorConfigRepresentation
        {
            Id = "c1",
            Alias = "redirector",
            Config = new Dictionary<string, string> { ["defaultProvider"] = "acme-idp" }
        });

        result.Result.Success.Should().BeTrue();
        handler.Requests[0].Method.Should().Be(HttpMethod.Put);
        handler.Requests[0].Uri.Should().Be($"{BaseAddress}admin/realms/acme/authentication/config/c1");
        using var body = JsonDocument.Parse(handler.Requests[0].Body!);
        body.RootElement.GetProperty("config").GetProperty("defaultProvider").GetString().Should().Be("acme-idp");
    }

    [Fact]
    public async Task UpdateAuthenticatorConfig_requires_id()
    {
        var (keycloak, handler) = Create(_ => new HttpResponseMessage(HttpStatusCode.NoContent));

        var act = () => keycloak.AuthenticationFlows.UpdateAuthenticatorConfig("acme", new AuthenticatorConfigRepresentation { Alias = "x" });

        await act.Should().ThrowAsync<ArgumentException>();
        handler.Requests.Should().BeEmpty();
    }

    [Theory]
    [InlineData("config", "admin/realms/acme/authentication/config/c1")]
    [InlineData("execution", "admin/realms/acme/authentication/executions/c1")]
    [InlineData("flow", "admin/realms/acme/authentication/flows/c1")]
    public async Task Delete_methods_call_delete_on_the_id(string kind, string expectedPath)
    {
        var (keycloak, handler) = Create(_ => new HttpResponseMessage(HttpStatusCode.NoContent));
        var flows = keycloak.AuthenticationFlows;

        var result = kind switch
        {
            "config" => await flows.DeleteAuthenticatorConfig("acme", "c1"),
            "execution" => await flows.DeleteExecution("acme", "c1"),
            _ => await flows.DeleteFlow("acme", "c1")
        };

        result.Result.Success.Should().BeTrue();
        handler.Requests.Should().ContainSingle();
        handler.Requests[0].Method.Should().Be(HttpMethod.Delete);
        handler.Requests[0].Uri.Should().Be(BaseAddress + expectedPath);
    }

    [Fact]
    public async Task AddExecution_returns_the_new_execution_id()
    {
        var (keycloak, handler) = Create(_ =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Location = new Uri($"{BaseAddress}admin/realms/acme/authentication/executions/e-123");
            return response;
        });

        var result = await keycloak.AuthenticationFlows.AddExecution("acme", "browser acme", "identity-provider-redirector");

        result.Result.Success.Should().BeTrue();
        result.Identifier.Should().Be("e-123");
        handler.Requests[0].Uri.Should().Be($"{BaseAddress}admin/realms/acme/authentication/flows/browser%20acme/executions/execution");
    }

    [Fact]
    public async Task CopyFlow_returns_the_new_flow_id()
    {
        var (keycloak, handler) = Create(_ =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Location = new Uri($"{BaseAddress}admin/realms/acme/authentication/flows/f-456");
            return response;
        });

        var result = await keycloak.AuthenticationFlows.CopyFlow("acme", "first broker login", "first broker login acme");

        result.Result.Success.Should().BeTrue();
        result.Identifier.Should().Be("f-456");
        using var body = JsonDocument.Parse(handler.Requests[0].Body!);
        body.RootElement.GetProperty("newName").GetString().Should().Be("first broker login acme");
    }

    private sealed class StubHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _respond;

        public StubHandler(Func<HttpRequestMessage, HttpResponseMessage> respond) => _respond = respond;

        public List<(HttpMethod Method, string Uri, string? Body)> Requests { get; } = new();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var body = request.Content == null ? null : await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            Requests.Add((request.Method, request.RequestUri!.AbsoluteUri, body));
            return _respond(request);
        }
    }
}
