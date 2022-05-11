using System.ComponentModel.DataAnnotations;

namespace Application.Models.Role;

public class AddUserToRoleRequest
{
    [EmailAddress]
    public string Username { get; set; } = "";
}
