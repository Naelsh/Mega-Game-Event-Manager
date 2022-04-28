using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = "";
    [Required]
    public string LastName { get; set; } = "";
    [Required]
    public string Username { get; set; } = "";

    [JsonIgnore]
    public string PasswordHash { get; set; } = "";

    public List<Activity> Activities { get; set; } = new List<Activity>();
    public List<Role> Roles { get; set; } = new List<Role>();
}
