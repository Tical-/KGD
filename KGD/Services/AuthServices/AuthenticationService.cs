using BlazorEcommerce.Shared;
using Blazored.LocalStorage;
using KGD.Application.Contracts.AuthContracts;
using KGD.Application.DTO;
using KGD.AuthProviders;
using Microsoft.AspNetCore.Components.Authorization;

namespace KGD.Services.AuthServices;

public class AuthenticationService : IAuthenticationService
{
    private readonly AuthorizationService _authorizationService;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(
        AuthenticationStateProvider authStateProvider,
        AuthorizationService authorizationService,
        ILocalStorageService localStorage)
    {
        _authStateProvider = authStateProvider;
        _authorizationService = authorizationService;
        _localStorage = localStorage;
    }


    public async Task<ServiceResponse<string>> Login(LoginDTO model)
    {
        var login = new LoginModel
        {
            Email = model.Username,
            Password = model.Password,
        };
        var loginResult = await _authorizationService.Login(login);

        if (loginResult != null)
        {
            _localStorage.SetItemAsync("accessToken", loginResult.Data);
            ((AuthProvider)_authStateProvider).NotifyUserAuthentication(loginResult.Data);
        }
        return loginResult;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("accessToken");
        ((AuthProvider)_authStateProvider).NotifyUserLogout();
    }

}
