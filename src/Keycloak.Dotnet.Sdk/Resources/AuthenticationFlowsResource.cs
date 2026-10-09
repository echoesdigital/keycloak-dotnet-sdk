namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Resources;

public class AuthenticationFlowsResource
{
    private readonly KeycloakInstance _instance;

    public AuthenticationFlowsResource(KeycloakInstance instance)
    {
        _instance = instance;
    }

    /// <summary>
    /// Lists all authentication flows in the realm
    /// </summary>
    public async Task<ResultResponseDto<List<AuthenticationFlowDto>>> List(string realm)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/flows", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<List<AuthenticationFlowDto>>
        {
            Data = await response.Content.ReadAsJsonAsync<List<AuthenticationFlowDto>>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    /// <summary>
    /// Gets a specific authentication flow by id.
    /// </summary>
    /// <remarks>
    /// Despite the parameter name, Keycloak serves <c>GET /authentication/flows/{id}</c> by flow <b>id</b>, not by
    /// alias: passing an alias returns 404. The signature is kept for compatibility; pass the flow id here, or use
    /// <see cref="GetByAlias"/> to look a flow up by alias.
    /// </remarks>
    public async Task<ResultResponseDto<AuthenticationFlowDto>> Get(string realm, string flowAlias)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/flows/{Uri.EscapeDataString(flowAlias)}", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<AuthenticationFlowDto>
        {
            Data = await response.Content.ReadAsJsonAsync<AuthenticationFlowDto>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    /// <summary>
    /// Gets a top-level authentication flow by alias. Keycloak has no endpoint by alias, so the realm flows are
    /// listed and filtered (ordinal comparison).
    /// </summary>
    /// <returns>
    /// <c>Success</c> reflects the list request. When the request succeeds and no flow has the alias,
    /// <c>Success</c> is true and <c>Data</c> is null.
    /// </returns>
    public async Task<ResultResponseDto<AuthenticationFlowDto>> GetByAlias(string realm, string flowAlias)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/flows", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        var flows = response.IsSuccessStatusCode ? await response.Content.ReadAsJsonAsync<List<AuthenticationFlowDto>>().ConfigureAwait(false) : null;

        return new ResultResponseDto<AuthenticationFlowDto>
        {
            Data = flows?.FirstOrDefault(f => string.Equals(f.Alias, flowAlias, StringComparison.Ordinal)),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    /// <summary>
    /// Deletes an authentication flow by id. Built-in flows cannot be deleted, and a flow in use by the realm or
    /// by an identity provider is refused by Keycloak.
    /// </summary>
    public async Task<ResultResponseDto> DeleteFlow(string realm, string flowId)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/flows/{Uri.EscapeDataString(flowId)}", UriKind.Relative);

        using var response = await _instance.HttpClient.DeleteAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    /// <summary>
    /// Copy existing authentication flow under a new name The new name is given as 'newName' attribute
    /// </summary>
    /// <returns><c>Identifier</c> carries the id of the new flow when Keycloak returns it in the Location header.</returns>
    public async Task<ResultResponseDto> CopyFlow(string realm, string sourceFlowAlias, string newFlowName)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/flows/{Uri.EscapeDataString(sourceFlowAlias)}/copy", UriKind.Relative);

        var copyRequest = new CopyFlowRequestDto { NewName = newFlowName };

        using var response = await _instance.HttpClient.PostJsonAsync(uri, copyRequest).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            },
            Identifier = response.Headers.Location?.Segments.LastOrDefault()
        };
    }

    /// <summary>
    /// Gets executions (steps) of a specific flow
    /// </summary>
    public async Task<ResultResponseDto<List<AuthenticationExecutionInfoRepresentationDto>>> GetExecutions(string realm, string flowAlias)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/flows/{Uri.EscapeDataString(flowAlias)}/executions", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<List<AuthenticationExecutionInfoRepresentationDto>>
        {
            Data = await response.Content.ReadAsJsonAsync<List<AuthenticationExecutionInfoRepresentationDto>>().ConfigureAwait(false),
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    /// <summary>
    /// Adds an execution (step) to a flow
    /// </summary>
    /// <returns><c>Identifier</c> carries the id of the new execution when Keycloak returns it in the Location header.</returns>
    public async Task<ResultResponseDto> AddExecution(string realm, string flowAlias, string provider)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/flows/{Uri.EscapeDataString(flowAlias)}/executions/execution", UriKind.Relative);

        var addRequest = new AddExecutionRequestDto { Provider = provider };

        using var response = await _instance.HttpClient.PostJsonAsync(uri, addRequest).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            },
            Identifier = response.Headers.Location?.Segments.LastOrDefault()
        };
    }

    /// <summary>
    /// Deletes an execution (step) by id
    /// </summary>
    public async Task<ResultResponseDto> DeleteExecution(string realm, string executionId)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/executions/{Uri.EscapeDataString(executionId)}", UriKind.Relative);

        using var response = await _instance.HttpClient.DeleteAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    /// <summary>
    /// Updates authentication executions of a Flow
    /// </summary>
    public async Task<ResultResponseDto> UpdateExecutionInfo(string realm, string flowAlias, AuthenticationExecutionInfoRepresentationDto executionInfo)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/flows/{Uri.EscapeDataString(flowAlias)}/executions", UriKind.Relative);

        using var response = await _instance.HttpClient.PutJsonAsync(uri, executionInfo).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    /// <summary>
    /// Update execution with new configuration
    /// </summary>
    public async Task<ResultResponseDto> CreateAuthenticatorConfig(string realm, string executionId, AuthenticatorConfigRepresentation config)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/executions/{executionId}/config", UriKind.Relative);

        using var response = await _instance.HttpClient.PostJsonAsync(uri, config).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            },
            Identifier = response.Headers.Location?.Segments.LastOrDefault()
        };
    }

    /// <summary>
    /// Gets an authenticator configuration by id (the <c>authenticationConfig</c> of an execution)
    /// </summary>
    public async Task<ResultResponseDto<AuthenticatorConfigRepresentation>> GetAuthenticatorConfig(string realm, string configId)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/config/{Uri.EscapeDataString(configId)}", UriKind.Relative);

        using var response = await _instance.HttpClient.GetAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto<AuthenticatorConfigRepresentation>
        {
            Data = response.IsSuccessStatusCode ? await response.Content.ReadAsJsonAsync<AuthenticatorConfigRepresentation>().ConfigureAwait(false) : null,
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    /// <summary>
    /// Updates an authenticator configuration. <see cref="AuthenticatorConfigRepresentation.Id"/> is required.
    /// </summary>
    public async Task<ResultResponseDto> UpdateAuthenticatorConfig(string realm, AuthenticatorConfigRepresentation config)
    {
        if (string.IsNullOrWhiteSpace(config.Id)) throw new ArgumentException("Authenticator config id is required.", nameof(config));

        var uri = new Uri($"admin/realms/{realm}/authentication/config/{Uri.EscapeDataString(config.Id)}", UriKind.Relative);

        using var response = await _instance.HttpClient.PutJsonAsync(uri, config).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }

    /// <summary>
    /// Deletes an authenticator configuration by id
    /// </summary>
    public async Task<ResultResponseDto> DeleteAuthenticatorConfig(string realm, string configId)
    {
        var uri = new Uri($"admin/realms/{realm}/authentication/config/{Uri.EscapeDataString(configId)}", UriKind.Relative);

        using var response = await _instance.HttpClient.DeleteAsync(uri).ConfigureAwait(false);

        return new ResultResponseDto
        {
            Result =
            {
                Success = response.IsSuccessStatusCode,
                Message = !response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : null
            }
        };
    }
}
