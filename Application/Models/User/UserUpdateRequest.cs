namespace Application.Models.User;

public class UserUpdateRequest
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public int ActivityId { get; set; }
    public int RoleId { get; set; }
}
