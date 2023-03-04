using KGD.Authorization.Models.DTO;
using System.Security.Claims;

namespace KGD.Authorization.Repositories.Abstract;

public interface ITokenService
{
    TokenResponse GetToken(IEnumerable<Claim> claim);
    string GetRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
