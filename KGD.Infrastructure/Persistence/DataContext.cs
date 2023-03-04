using KGD.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KGD.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("KGDDatabase"));
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Report>? Reports { get; set; }
    }
}
