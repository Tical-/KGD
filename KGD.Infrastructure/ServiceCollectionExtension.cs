using KGD.Application.Contracts;
using KGD.Application.Contracts.AuthContracts;
using KGD.Application.Services.AuthServices;
using KGD.Domain.Entity;
using KGD.Infrastructure.Persistence;
using KGD.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KGD.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, string url)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContextPool<DataContext>(options => options
                .UseSqlite(url));

            services.AddDbContextPool<AuthContextImp>(options => options
               .UseSqlite(url));

            services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<AuthContextImp>()
           .AddDefaultTokenProviders();

            services.AddScoped<IAuthContext, AuthContextImp>();
        }
    }
}
