using KGD.Application.Contracts;
using KGD.Infrastructure.Persistence;
using KGD.Infrastructure.Repository;
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
        }
    }
}
