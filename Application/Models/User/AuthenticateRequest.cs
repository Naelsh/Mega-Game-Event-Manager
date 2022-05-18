using System.ComponentModel.DataAnnotations;

namespace Application.Models.User;

public class AuthenticateRequest
{
    [Required]
    [EmailAddress]
    public string Username { get; set; } = "";

    [Required]
    [StringLength(255, MinimumLength = 6)]
    public string Password { get; set; } = "";
}
