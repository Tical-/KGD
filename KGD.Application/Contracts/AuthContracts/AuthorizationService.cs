using BlazorEcommerce.Shared;
using KGD.Application.DTO;
using KGD.Domain.Entity;

namespace KGD.Application.Contracts.AuthContracts;

public interface AuthorizationService
{
    //Task<Status> ChangePassword(ChangePasswordModel model);
    Task<ServiceResponse<string>> Login(LoginModel model);
    //Task<Status> Register(RegistrationModel model);
    //Task<Status> RegistrationAdmin(RegistrationModel model);
    Task<bool> UserExists(string email);
    int GetUserId();
    string GetUserEmail();
    Task<User> GetUserByEmail(string email);
}
