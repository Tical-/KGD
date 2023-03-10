using Blazored.LocalStorage;
using KGD.Application;
using KGD.Application.Contracts.AuthContracts;
using KGD.AuthProviders;
using KGD.Data;
using KGD.Infrastructure;
using KGD.Services;
using KGD.Services.AuthServices;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration.GetConnectionString("KGDDatabase"));

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<AuthorizationService, AuthorizationServiceImp>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<KGDService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.AddJwt();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
