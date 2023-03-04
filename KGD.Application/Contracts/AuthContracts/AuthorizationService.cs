using KGD.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KGD.Application.Contracts.AuthContracts;

public interface AuthorizationService
{
    Task<Status> ChangePassword(ChangePasswordModel model);
    Task<LoginResponse> Login([FromBody] LoginModel model);
    Task<Status> Registration([FromBody] RegistrationModel model);
    Task<Status> RegistrationAdmin([FromBody] RegistrationModel model);
}
