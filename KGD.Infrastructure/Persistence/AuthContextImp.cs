using KGD.Application.Contracts.AuthContracts;
using KGD.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KGD.Infrastructure.Persistence;

public class AuthContextImp : IdentityDbContext<ApplicationUser>, IAuthContext
{
    public AuthContextImp(DbContextOptions<AuthContextImp> options) : base(options)
    {

    }
    public DbSet<TokenInfo> TokenInfo { get; set; }

    #region Methods
    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
    #endregion
}
