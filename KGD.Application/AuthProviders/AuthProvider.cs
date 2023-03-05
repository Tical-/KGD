using Blazored.LocalStorage;
using KGD.Application.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Data;
using System.Security.Claims;

namespace KGD.Application.AuthProviders;

public class AuthProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;

    public AuthProvider(
        ILocalStorageService localStorage
        )
    {
        _localStorage = localStorage;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");
        if (string.IsNullOrEmpty(token))
        {
            return _anonymous;
        }
        return new AuthenticationState(new ClaimsPrincipal(
              new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
    }

    public void NotifyUserAuthentication(string token)
    {
        var authUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
        var authState = Task.FromResult(new AuthenticationState(authUser));
        NotifyAuthenticationStateChanged(authState);

    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}
