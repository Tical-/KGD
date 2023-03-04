using KGD.Models;

namespace KGD.Authorization.Services.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponse> Login(LoginDTO model);
    Task Logout();
}