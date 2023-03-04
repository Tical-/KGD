using System.ComponentModel.DataAnnotations;

namespace KGD.Application.DTO;

public class LoginDTO
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
}
