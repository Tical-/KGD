using KGD.Models;

namespace KGD.Services;

public interface IAuthenticationService
{
    Task<LoginResponse> Login(LoginDTO model);
    Task Logout();
}