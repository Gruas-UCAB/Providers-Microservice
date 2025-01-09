namespace ProvidersMicroservice.core.Application
{
    public interface ITokenAuthenticationService
    {
        TokenResponse Authenticate(string username, string userRole);
    }

    public class TokenResponse(string token, int expiresIn)
    {
        public string AccessToken { get; } = token;
        public int ExpiresIn { get; } = expiresIn;
    }
}
