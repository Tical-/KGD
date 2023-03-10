using System.ComponentModel.DataAnnotations;

namespace KGD.Application.DTO;

public class LoginModel
{
    [Required]
    public string? Email { get; set; }
   
    [Required]
    public string? Password { get; set; }
}
