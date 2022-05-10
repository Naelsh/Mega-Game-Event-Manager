using System.ComponentModel.DataAnnotations;

namespace Application.Models.Role;

public class AddUserToRoleRequest
{
    [Required]
    [EmailAddress]
    public string Username { get; set; } = "";
}
