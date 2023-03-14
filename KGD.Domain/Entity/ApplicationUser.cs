using Microsoft.AspNetCore.Identity;

namespace KGD.Domain.Entity;

public class ApplicationUser: IdentityUser
{
    public string? Name { get; set; }
}
