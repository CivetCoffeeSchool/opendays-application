using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Model.Entities.Users;

namespace Domain.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<AuthResult> Authenticate(string username, string password)
    {
        // var user = await Task.Run(() => _userRepository.GetByUsername(username));
        //
        // if (user == null || !VerifyPassword(password, user.PasswordHash))
        //     return new AuthResult { Success = false, Message = "Invalid username or password" };
        //
        // var token = GenerateJwtToken(user);
        // var refreshToken = GenerateRefreshToken();
        //
        // // Save refresh token to user record
        // user.RefreshToken = refreshToken;
        // user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        // _userRepository.Update(user);
        //
        // return new AuthResult
        // {
        //     Success = true,
        //     Token = token,
        //     RefreshToken = refreshToken,
        //     User = new UserDto
        //     {
        //         Username = user.Username,
        //         Role = user.UserType,
        //         PersonId = user.PersonId
        //     }
        // };
        throw new NotImplementedException();
    }

    public async Task<AuthResult> RefreshToken(string token, string refreshToken)
    {
        // var principal = GetPrincipalFromExpiredToken(token);
        // var username = principal.Identity?.Name;
        //
        // var user = await Task.Run(() => _userRepository.GetByUsername(username));
        // if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow)
        //     return new AuthResult { Success = false, Message = "Invalid token" };
        //
        // var newToken = GenerateJwtToken(user);
        // var newRefreshToken = GenerateRefreshToken();
        //
        // user.RefreshToken = newRefreshToken;
        // _userRepository.Update(user);
        //
        // return new AuthResult
        // {
        //     Success = true,
        //     Token = newToken,
        //     RefreshToken = newRefreshToken
        // };
        throw new NotImplementedException();
    }

    public async Task<bool> RevokeToken(string username)
    {
        // var user = await Task.Run(() => _userRepository.GetByUsername(username));
        // if (user == null) return false;
        //
        // user.RefreshToken = null;
        // _userRepository.Update(user);
        // return true;
        throw new NotImplementedException();
    }

    private string GenerateJwtToken(AUser user)
    {
        // var tokenHandler = new JwtSecurityTokenHandler();
        // var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
        //
        // var tokenDescriptor = new SecurityTokenDescriptor
        // {
        //     Subject = new ClaimsIdentity(new[]
        //     {
        //         new Claim(ClaimTypes.Name, user.Username),
        //         new Claim(ClaimTypes.Role, user.UserType),
        //         new Claim("personId", user.PersonId)
        //     }),
        //     Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
        //     SigningCredentials = new SigningCredentials(
        //         new SymmetricSecurityKey(key),
        //         SecurityAlgorithms.HmacSha256Signature)
        // };
        //
        // var token = tokenHandler.CreateToken(tokenDescriptor);
        // return tokenHandler.WriteToken(token);
        throw new NotImplementedException();
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        // var tokenValidationParameters = new TokenValidationParameters
        // {
        //     ValidateAudience = false,
        //     ValidateIssuer = false,
        //     ValidateIssuerSigningKey = true,
        //     IssuerSigningKey = new SymmetricSecurityKey(
        //         Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"])),
        //     ValidateLifetime = false
        // };
        //
        // var tokenHandler = new JwtSecurityTokenHandler();
        // var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
        // return principal;
        throw new NotImplementedException();
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        // Implement your password verification logic here
        // For example, using BCrypt:
        // return BCrypt.Verify(password, storedHash);
        return password == storedHash; // Simple example - use proper hashing in production
    }
}

//TODO
//Need to be fixed, for now commented