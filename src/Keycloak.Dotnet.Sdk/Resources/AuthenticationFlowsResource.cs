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
    /// Gets a specific authentication flow by alias
    /// </summary>
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
    /// Copy existing authentication flow under a new name The new name is given as 'newName' attribute
    /// </summary>
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
            }
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
}
