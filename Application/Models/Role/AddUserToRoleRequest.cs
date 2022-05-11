using System.ComponentModel.DataAnnotations;

namespace Application.Models.Role;

public class AddUserToRoleRequest
{
    [EmailAddress]
    public string Username { get; set; } = "";
    public int ActivityId { get; set; } = 0;
}
