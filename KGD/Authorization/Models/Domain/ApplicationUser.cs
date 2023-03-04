using Microsoft.AspNetCore.Identity;

namespace KGD.Authorization.Models.Domain;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
}
