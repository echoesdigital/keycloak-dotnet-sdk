namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Exceptions;

public class KeycloakException : Exception
{
    public KeycloakException(string message) : base(message)
    {
    }
}