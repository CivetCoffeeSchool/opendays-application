namespace Domain.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResult> Authenticate(string username, string password);
    Task<AuthResult> RefreshToken(string token, string refreshToken);
    Task<bool> RevokeToken(string username);
}