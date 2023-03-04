using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using KGD.Application.Contracts;
using KGD.Application.Services.UserService;

namespace KGD.Application
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
