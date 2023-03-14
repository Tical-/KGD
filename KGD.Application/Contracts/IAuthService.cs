using BlazorEcommerce.Shared;
using KGD.Application.DTO;
using KGD.Domain.Entity;

namespace KGD.Application.Contracts;

public interface IAuthService
{
    Task<ServiceResponse<string>> Login(LoginModel model);
    Task<ServiceResponse<int>> Register(User user);
}
