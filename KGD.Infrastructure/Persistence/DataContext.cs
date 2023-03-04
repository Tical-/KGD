using KGD.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace KGD.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Report>? Reports { get; set; }
    }
}
