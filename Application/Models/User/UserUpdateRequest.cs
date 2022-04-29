using System.ComponentModel.DataAnnotations;

namespace Application.Models.User;

public class UserUpdateRequest
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    [EmailAddress]
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}
