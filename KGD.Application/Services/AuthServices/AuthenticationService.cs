using Blazored.LocalStorage;
using KGD.Application.AuthProviders;
using KGD.Application.Contracts.AuthContracts;
using KGD.Application.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace KGD.Application.Services.AuthServices;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly AuthorizationService _authorizationService;
    //private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly string baseUrl;
   // private AuthProvider _authStateProvider;

    public AuthenticationService(
        //AuthProvider authStateProvider,
        //HttpClient httpClient,
        //AuthenticationStateProvider authStateProvider,
        AuthorizationService authorizationService,
        ILocalStorageService localStorage)
    {
        //this._httpClient = httpClient;
        //this._authStateProvider = authStateProvider;
        //_authStateProvider = authStateProvider;
        _authorizationService = authorizationService;
        _localStorage = localStorage;
        //  baseUrl = "https://localhost:7010/api/authorization";
    }


    public async Task<LoginResponse> Login(LoginDTO model)
    {
        var login = new LoginModel
        {
            Username = model.Username,
            Password = model.Password,
        };
        var loginResult = await _authorizationService.Login(login);
        if (loginResult.StatusCode == 0)
            return new LoginResponse { StatusCode = 0, Message = "Server error" };

        var loginResponseContent = new LoginResponse
        {
            Token = loginResult.Token,
            RefreshToken = loginResult.RefreshToken,
            Expiration = loginResult.Expiration,
            Name = loginResult.Name,
            Username = loginResult.Username,
        };

        if (loginResponseContent != null)
        {
            _localStorage.SetItemAsync("accessToken", loginResponseContent.Token);
           //((AuthProvider)_authStateProvider).NotifyUserAuthentication(loginResponseContent.Token);
        }
        return loginResponseContent;

    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("accessToken");
      //  ((AuthProvider)_authStateProvider).NotifyUserLogout();
    }

}
