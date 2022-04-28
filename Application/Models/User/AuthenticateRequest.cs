using System.ComponentModel.DataAnnotations;

namespace Application.Models.User;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
