using KGD.Application.DTO;

namespace KGD.Application.Contracts.AuthContracts;

public interface IAuthenticationService
{
    Task<LoginResponse> Login(LoginDTO model);
    Task Logout();
}