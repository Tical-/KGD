using Blazored.LocalStorage;
using KGD.AuthProviders;
using KGD.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace KGD.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    //private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly string baseUrl;

    public AuthenticationService(
        //HttpClient httpClient,AuthenticationStateProvider authStateProvider,
        ILocalStorageService localStorage)
    {
        //this._httpClient = httpClient;
        //this._authStateProvider = authStateProvider;
        this._localStorage = localStorage;
      //  baseUrl = "https://localhost:7010/api/authorization";
    }


    public async Task<LoginResponse> Login(LoginDTO model)
    {
        
        var loginResult = await _httpClient.PostAsJsonAsync($"{baseUrl}/login",model);
        if (!loginResult.IsSuccessStatusCode)
            return new LoginResponse { StatusCode = 0, Message = "Server error" };
        var loginResponseContent = await loginResult.Content.ReadFromJsonAsync<LoginResponse>();
        //if (loginResponseContent != null)
        //{
        //    _localStorage.SetItemAsync("accessToken", loginResponseContent.Token);
        //    ((AuthProvider)_authStateProvider).NotifyUserAuthentication(loginResponseContent.Token);
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResponseContent.Token);
        //}
        return loginResponseContent;

    }

    public async Task Logout()
    {
        //await _localStorage.RemoveItemAsync("accessToken");
        //((AuthProvider)_authStateProvider).NotifyUserLogout();
        //_httpClient.DefaultRequestHeaders.Authorization = null;

    }

}
