using System.ComponentModel.DataAnnotations;

namespace Application.Models.User;

public class UserUpdateRequest
{
    [StringLength(255)]
    public string FirstName { get; set; } = "";
    [StringLength(255)]
    public string LastName { get; set; } = "";
    [EmailAddress]
    public string Username { get; set; } = "";
    [StringLength(255, MinimumLength = 6)]
    public string Password { get; set; } = "";
}
