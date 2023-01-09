using System.Security.Claims;

namespace KlondikeWebApi.Token;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims, string clientUrl, string jwtIssuer, string jwtSecretKey);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string jwtSecretKey, string token);
}
