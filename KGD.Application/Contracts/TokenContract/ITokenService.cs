using KGD.Application.DTO;
using System.Security.Claims;

namespace KGD.Application.Contracts.TokenContract;

public interface ITokenService
{
    TokenResponse GetToken(IEnumerable<Claim> claim);
    string GetRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
