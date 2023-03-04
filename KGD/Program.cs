using Blazored.LocalStorage;
using KGD.Application;
using KGD.Application.Contracts;
using KGD.Data;
using KGD.Services;
using KGD.Infrastructure;
using KGD.Infrastructure.Persistence;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddRazorPages();
    builder.Services.AddApplicationLayer();

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration.GetConnectionString("KGDDatabase"));
    builder.Services.AddInfrastructureLayer(builder.Configuration.GetConnectionString("KGDDatabase"));
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

}
catch (Exception e)
{

}
