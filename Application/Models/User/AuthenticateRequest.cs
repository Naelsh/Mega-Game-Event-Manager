using System.ComponentModel.DataAnnotations;

namespace Application.Models.User;

public class AuthenticateRequest
{
    [EmailAddress]
    [Required]
    public string Username { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
