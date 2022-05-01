using System.ComponentModel.DataAnnotations;

namespace Application.Models.User;

public class RegisterRequest
{
    [Required]
    [StringLength(255)]
    public string FirstName { get; set; } = "";

    [Required]
    [StringLength(255)]
    public string LastName { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Username { get; set; } = "";

    [Required]
    [StringLength(255, MinimumLength = 6)]
    public string Password { get; set; } = "";
}
