using BlazorEcommerce.Shared;
using KGD.Application.DTO;

namespace KGD.Application.Contracts.AuthContracts;

public interface IAuthenticationService
{
    Task<ServiceResponse<string>> Login(LoginDTO model);
    Task Logout();
}