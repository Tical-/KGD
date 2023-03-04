using KGD.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KGD.Application.Contracts.AuthContracts
{
    public interface IAuthContext
    {
        DbSet<TokenInfo> TokenInfo { get; set; }
        Task<int> SaveChangesAsync();
    }
}
